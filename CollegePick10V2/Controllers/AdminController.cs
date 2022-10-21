using AutoMapper;
using CollegePick10V2.Entities;
using CollegePick10V2.Entities.Models;
using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;

namespace CollegePick10V2.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public AdminController(ApplicationContext context, IConfiguration config, IMapper mapper, UserManager<User> userManager, IEmailSender emailSender)
        {
            _context = context;
            _config = config;
            _mapper = mapper;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddGames()
        {
            return View(await OpenFeed());
        }

        public async Task<IActionResult> AddOdds()
        {
            var maxWeek = await _context.PickArchives.MaxAsync(x => x.WeekNumber);
            if (maxWeek == null) maxWeek = 0;
            var picks = await _context.Picks.ToListAsync();
            foreach(var pick in picks)
            {
                var archive = _mapper.Map<PickArchive>(pick);
                archive.WeekNumber = maxWeek + 1;
                _context.PickArchives.Add(archive);
            }
            var games = await _context.CurrentWeeks.ToListAsync();
            foreach(var game in games)
            {
                var archive = _mapper.Map<GameArchive>(game);
                archive.WeekNumber = maxWeek + 1;
                _context.GameArchives.Add(archive);
            }
            var newGames = await OpenFeed();
            _context.Picks.RemoveRange(_context.Picks);
            _context.CurrentWeeks.RemoveRange(_context.CurrentWeeks);
            await _context.SaveChangesAsync();
            await _context.CurrentWeeks.AddRangeAsync(newGames);
            await _context.SaveChangesAsync();

            await SendEmail("College Pick 10 Games Updated", "THIS WEEKS GAMES HAVE BEEN UPDATED!");

            return View(newGames);
        }

        public IActionResult BulkEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendBulkEmail(EmailModel email)
        {
            var users = await _userManager.GetUsersInRoleAsync("Member");
            foreach (var user in users)
            {
                var message = new Message(new string[] { user.Email }, email.Subject, email.Body, null);
                await _emailSender.SendEmailAsync(message);
            }
            return RedirectToAction(nameof(AdminController.Index), "Admin");
        }

        public async Task<IActionResult> UserLIst()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> MarkPaid(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            user.PaidAmount = _config.GetValue<int>("AppSettings:PaymentAmount") * _config.GetValue<int>("AppSettings:TotalWeeks");
            await _context.SaveChangesAsync();
            return Json(new { result = true });
        }

        public async Task<IActionResult> MakeAdmin(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await _userManager.AddToRoleAsync(user, "Administrator");
            return Json(new { result = true });
        }

        public async Task<IActionResult> SendPickReminderEmail()
        {
            await SendEmail("College Pick 10 Pick Reminder", "THIS IS YOUR WEEKLY PICK REMINDER.");
            return RedirectToAction(nameof(AdminController.Index), "Admin");
        }

        private async Task<int> SendEmail(string Subject, string mainText)
        {
            var firstGame = await _context.CurrentWeeks.OrderBy(x => x.GameTime).FirstOrDefaultAsync(x => !x.Locked);
            var users = await _userManager.GetUsersInRoleAsync("Member");
            foreach (var user in users)
            {
                var gameTime = ConvertTimeZone(firstGame.GameTime, user);
                var url = "http://collegepick10.com/templates/emailTemplate.html";
                var web = new HtmlWeb();
                var doc = web.Load(url);
                var emailText = string.Format(doc.ParsedText, gameTime.ToShortDateString(), gameTime.ToShortTimeString(), mainText);
                var message = new Message(new string[] { user.Email }, Subject, emailText, null);
                await _emailSender.SendEmailAsync(message);
            }
            return 1;
        }
        private DateTime ConvertTimeZone(DateTime dateToConvert, User user)
        {
            if (User.Identity.IsAuthenticated)
            {
                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                var dateToReturn = TimeZoneInfo.ConvertTimeToUtc(dateToConvert, timeZone);
                timeZone = TimeZoneInfo.FindSystemTimeZoneById(user.TimeZone);
                return TimeZoneInfo.ConvertTimeFromUtc(dateToReturn, timeZone);
            }
            return dateToConvert;

        }

        private async Task<List<CurrentWeek>> OpenFeed()
        {
            var feedResults = new List<CurrentWeek>();
            var url = "https://api.the-odds-api.com/v4/sports/americanfootball_ncaaf/odds/?regions=us&oddsFormat=american&markets=spreads,totals&apiKey=49fba82d7d6461d378d6b388b2d56e10";
            
            //var url = "https://www.collegepick10.com/feed.txt";
            var client = new WebClient();
            var response = "{\"events\":" + client.DownloadString(url) + "}";
            OddsFeedResponse json = JsonConvert.DeserializeObject<OddsFeedResponse>(response);


            foreach (var item in json.events)
            {
                var currentGame = new CurrentWeek();
                var homeTeam = await _context.Teams.FirstOrDefaultAsync(x => x.FulleName == item.home_team); 
                var awayTeam = await _context.Teams.FirstOrDefaultAsync(x => x.FulleName == item.away_team);
                if (homeTeam == null) homeTeam = await CreateTeam(item.home_team);
                if (awayTeam == null) awayTeam = await CreateTeam(item.away_team);
                TimeZoneInfo centralZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                currentGame.GameTime = TimeZoneInfo.ConvertTimeFromUtc(item.commence_time, centralZone);
                currentGame.HomeTeam = homeTeam.TeamID;
                currentGame.FavoriteScore = 0;
                currentGame.UnderdogScore = 0;
                if (item.commence_time > DateTime.Now.AddDays(_config.GetValue<int>("AppSettings:OddsCutoffDays"))) continue;
                var bookmaker = item.bookmakers.FirstOrDefault(x => x.key == "williamhill_us");
                if (bookmaker == null) continue;
                var markets = bookmaker.markets.ToList();
                foreach(var market in markets)
                {                    
                    var line = market.outcomes.FirstOrDefault();
                    if(market.key == "spreads")
                    {
                        currentGame.PointSpread = Math.Abs(line.point);
                        if (line.point > 0 && line.name == item.home_team)
                        {
                            currentGame.FavoriteID = awayTeam.TeamID;
                            currentGame.UnderDogID = homeTeam.TeamID;
                        }
                        if (line.point < 0 && line.name == item.home_team)
                        {
                            currentGame.FavoriteID = homeTeam.TeamID;
                            currentGame.UnderDogID = awayTeam.TeamID;
                        }
                        if (line.point > 0 && line.name == item.away_team)
                        {
                            currentGame.FavoriteID = homeTeam.TeamID;
                            currentGame.UnderDogID = awayTeam.TeamID;
                        }
                        if (line.point < 0 && line.name == item.away_team)
                        {
                            currentGame.FavoriteID = awayTeam.TeamID;
                            currentGame.UnderDogID = homeTeam.TeamID;
                        }
                        if (line.point == 0)
                        {
                            currentGame.FavoriteID = homeTeam.TeamID;
                            currentGame.UnderDogID = awayTeam.TeamID;
                        }
                    }

                    if(market.key == "totals")
                    {
                        currentGame.OverUnder = Math.Abs(line.point);
                    }
                }             
                
                feedResults.Add(currentGame);
            }

            return feedResults;
        }

        private async Task<Teams> CreateTeam(string teamName)
        {
            var newTeam = new Teams();
            newTeam.FulleName = teamName;
            newTeam.EspnName = "Needs Name";
            newTeam.Abbreviation = "Needs Name";
            await _context.Teams.AddAsync(newTeam);
            await _context.SaveChangesAsync();
            return newTeam;

        }

    }
}
