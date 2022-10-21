using AutoMapper;
using CollegePick10V2.Entities;
using CollegePick10V2.Entities.Models;
using CollegePick10V2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CollegePick10V2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;

        public HomeController(ApplicationContext context, IMapper mapper, IConfiguration config, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {            
            return View(await GetScheduleData());
        }
        public async Task<IActionResult> GetSchedule()
        {            
            return PartialView("Schedule", await GetScheduleData());
        }
        [Authorize]
        public async Task<IActionResult> EnterPicks()
        {            
            return View(await GetPickInfo());
        }

        [Authorize]
        public IActionResult Payment()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> GetPicks()
        {
            return PartialView("Picks", await GetPickInfo());
        }

        [Authorize]
        public async Task<IActionResult> RefreshPicks()
        {
            await LockGames();
            await GetScores();
            return PartialView("Picks", await GetPickInfo());
        }

        [HttpGet]
        public async Task<IActionResult> GetResults(int? id = null)
        {
            return PartialView("Results", await GetAllPicks(id));
        }

        public IActionResult Rules()
        {
            var settings = new AppSettingsModel();
            settings.PaymentAmount = _config.GetValue<Double>("AppSettings:PaymentAmount") * _config.GetValue<Double>("AppSettings:TotalWeeks");
            settings.SeasonStart = _config.GetValue<string>("AppSettings:SeasonStart");
            settings.SeasonEnd = _config.GetValue<string>("AppSettings:SeasonEnd");
            settings.PaymentCutoff = _config.GetValue<string>("AppSettings:PaymentCutoff");
            return View(settings);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> AddPick(int gameID, string pickType)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity.Name);
            var currentPicks = await _context.Picks.Where(x => x.UserID == user.Id).ToListAsync();
            var pick = new Pick();
            switch (pickType)
            {
                case "favorite": pick.PickTypeID = (int)Enums.PickTypes.Favorite; break;
                case "underdog": pick.PickTypeID = (int)Enums.PickTypes.Underdog; break;
                case "push": pick.PickTypeID = (int)Enums.PickTypes.SpreadPush; break;
                case "over": pick.PickTypeID = (int)Enums.PickTypes.Over; break;
                case "under": pick.PickTypeID = (int)Enums.PickTypes.Under; break;
                case "overunderpush": pick.PickTypeID = (int)Enums.PickTypes.OverUnderPush; break;
            }
            var picksToRemove = new List<Pick>();
            if(pick.PickTypeID == (int)Enums.PickTypes.Favorite || pick.PickTypeID == (int)Enums.PickTypes.Underdog || pick.PickTypeID == (int)Enums.PickTypes.SpreadPush)
            {
                picksToRemove = await _context.Picks.Where(x => x.UserID == user.Id && x.GameID == gameID && (x.PickTypeID == (int)Enums.PickTypes.Favorite || x.PickTypeID == (int)Enums.PickTypes.Underdog || x.PickTypeID == (int)Enums.PickTypes.SpreadPush)).ToListAsync();
            }
            if (pick.PickTypeID == (int)Enums.PickTypes.Over || pick.PickTypeID == (int)Enums.PickTypes.Under || pick.PickTypeID == (int)Enums.PickTypes.OverUnderPush)
            {
                picksToRemove = await _context.Picks.Where(x => x.UserID == user.Id && x.GameID == gameID && (x.PickTypeID == (int)Enums.PickTypes.Over || x.PickTypeID == (int)Enums.PickTypes.Under || x.PickTypeID == (int)Enums.PickTypes.OverUnderPush)).ToListAsync();
            }
            _context.Picks.RemoveRange(picksToRemove);
            _context.SaveChanges();
            currentPicks = await _context.Picks.Where(x => x.UserID == user.Id).ToListAsync();
            if (currentPicks.Count() >= 10) return Json(new { result = false, error = "You already have 10 picks selected" }); 
            
            pick.UserID = user.Id;
            pick.GameID = gameID;
            switch(pickType)
            {
                case "favorite": pick.PickTypeID = (int)Enums.PickTypes.Favorite; break;
                case "underdog": pick.PickTypeID = (int)Enums.PickTypes.Underdog; break;
                case "push": pick.PickTypeID = (int)Enums.PickTypes.SpreadPush; break;
                case "over": pick.PickTypeID = (int)Enums.PickTypes.Over; break;
                case "under": pick.PickTypeID = (int)Enums.PickTypes.Under; break;
                case "overunderpush": pick.PickTypeID = (int)Enums.PickTypes.OverUnderPush; break;
            }
            if (await _context.Picks.CountAsync(x => x.GameID == pick.GameID && x.UserID == pick.UserID && x.PickTypeID == pick.PickTypeID) == 0)
            {
                await _context.Picks.AddAsync(pick);
                await _context.SaveChangesAsync();
            }
            return Json(new { result = true });
        }

        [Authorize]
        public async Task<IActionResult> RemovePick(int gameID, string pickType)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity.Name);
            var pickTypeID = 0;
            switch (pickType)
            {
                case "favorite": pickTypeID = (int)Enums.PickTypes.Favorite; break;
                case "underdog": pickTypeID = (int)Enums.PickTypes.Underdog; break;
                case "push": pickTypeID = (int)Enums.PickTypes.SpreadPush; break;
                case "over": pickTypeID = (int)Enums.PickTypes.Over; break;
                case "under": pickTypeID = (int)Enums.PickTypes.Under; break;
                case "overunderpush": pickTypeID = (int)Enums.PickTypes.OverUnderPush; break;
            }
            var pick = await _context.Picks.FirstOrDefaultAsync(x => x.UserID == user.Id && x.GameID == gameID && x.PickTypeID == pickTypeID);
            if (pick != null)
            {
                _context.Picks.Remove(pick);
                await _context.SaveChangesAsync();
            }
            return Json(new { result = true });
        }

        [Authorize]
        public async void MarkAsPaid(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            user.PaidAmount = _config.GetValue<int>("AppSettings:PaymentAmount") * _config.GetValue<int>("AppSettings:TotalWeeks");
            await _context.SaveChangesAsync();
        }

        [Authorize]
        public async Task<IActionResult> PaymentSuccess()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            user.PaidAmount = _config.GetValue<int>("AppSettings:PaymentAmount") * _config.GetValue<int>("AppSettings:TotalWeeks");
            await _context.SaveChangesAsync();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ViewPicks(int? id = null)
        {
            return View(await GetAllPicks(id));
        }

        private async Task<List<ViewPicksModel>> GetAllPicks(int? id)
        {
            await LockGames();
            await GetScores();

            var me = new User();
            if(User.Identity.IsAuthenticated) me = await _userManager.FindByEmailAsync(User.Identity.Name);
            var pickList = new List<ViewPicksModel>();
            var users = await _context.Users.ToListAsync();
            var picks = await _context.Picks.ToListAsync();
            var pickArchives = await _context.PickArchives.ToListAsync();
            var currentWeek = await _context.CurrentWeeks.ToListAsync();
            var gameArchive = await _context.GameArchives.ToListAsync();
            var teams = await _context.Teams.ToListAsync();
            foreach(var user in users)
            {
                var userPicks = new ViewPicksModel();
                var maxweeks = gameArchive.Max(x => x.WeekNumber);
                if (maxweeks == null)
                    maxweeks = 0;
                userPicks.MaxWeeks = maxweeks;
                userPicks.FirstName = user.FirstName;
                userPicks.LastName = user.LastName;
                if (user.Id == me.Id)
                    userPicks.MyPicks = true;
                var schedule = new List<ScheduleModel>();
                var totalPoints = 0;
                if (id == null || id == 0 || id > maxweeks)
                {
                    userPicks.WeekNumber = (int)maxweeks+1;
                    var myPicks = picks.Where(x => x.UserID == user.Id).ToList();
                    foreach(var pick in myPicks)
                    {
                        var game = new ScheduleModel();
                        var currentGame = currentWeek.FirstOrDefault(x => x.GameID == pick.GameID);
                        game.GameID = pick.GameID;
                        var time = await ConvertTimeZone(currentGame.GameTime);
                        game.GameProgress = (currentGame.Locked ? currentGame.GameProgress : time.ToString("g"));
                        game.FavoriteScore = currentGame.FavoriteScore;
                        game.UnderdogScore = currentGame.UnderdogScore;
                        game.OverUnder = currentGame.OverUnder;
                        game.PointSpread = currentGame.PointSpread;
                        game.Favorite = teams.FirstOrDefault(x => x.TeamID == currentGame.FavoriteID).Abbreviation;
                        game.UnderDog = teams.FirstOrDefault(x => x.TeamID == currentGame.UnderDogID).Abbreviation;
                        if (pick.PickTypeID == (int)Enums.PickTypes.Favorite || pick.PickTypeID == (int)Enums.PickTypes.Underdog || pick.PickTypeID == (int)Enums.PickTypes.SpreadPush)
                            game.SpreadPickType = pick.PickTypeID;
                        if (pick.PickTypeID == (int)Enums.PickTypes.Over || pick.PickTypeID == (int)Enums.PickTypes.Under || pick.PickTypeID == (int)Enums.PickTypes.OverUnderPush)
                            game.OverUnderPickType = pick.PickTypeID;
                        game.Locked = currentGame.Locked;
                        if (game.Locked || userPicks.MyPicks)
                        {
                            if (pick.PickTypeID == (int)Enums.PickTypes.Favorite)
                            {
                                if (game.PointSpread >= (game.FavoriteScore - game.UnderdogScore))
                                    game.FavoriteCssClass = "bg-danger";
                                if (game.PointSpread < (game.FavoriteScore - game.UnderdogScore))
                                {
                                    game.FavoriteCssClass = "bg-success";
                                    if(game.Locked)
                                        totalPoints += 1;
                                }
                            }
                            if (pick.PickTypeID == (int)Enums.PickTypes.Underdog)
                            {
                                if (game.PointSpread <= (game.FavoriteScore - game.UnderdogScore))
                                    game.UnderdogCssClass = "bg-danger";
                                if (game.PointSpread > (game.FavoriteScore - game.UnderdogScore))
                                {
                                    game.UnderdogCssClass = "bg-success";
                                    if (game.Locked)
                                        totalPoints += 1;
                                }
                            }
                            if (pick.PickTypeID == (int)Enums.PickTypes.SpreadPush)
                            {
                                if (game.PointSpread != (game.FavoriteScore - game.UnderdogScore))
                                {
                                    game.FavoriteCssClass = "bg-danger";
                                    game.UnderdogCssClass = "bg-danger";
                                }
                                if (game.PointSpread == (game.FavoriteScore - game.UnderdogScore))
                                {
                                    game.FavoriteCssClass = "bg-success";
                                    game.UnderdogCssClass = "bg-success";
                                    if (game.Locked)
                                        totalPoints += 2;
                                }
                            }
                            if (pick.PickTypeID == (int)Enums.PickTypes.Over)
                            {
                                if (game.OverUnder >= (game.FavoriteScore + game.UnderdogScore))
                                    game.OverCssClass = "bg-danger";
                                if (game.OverUnder < (game.FavoriteScore + game.UnderdogScore))
                                {
                                    game.OverCssClass = "bg-success";
                                    if (game.Locked)
                                        totalPoints += 1;
                                }
                            }
                            if (pick.PickTypeID == (int)Enums.PickTypes.Under)
                            {
                                if (game.OverUnder <= (game.FavoriteScore + game.UnderdogScore))
                                    game.UnderCssClass = "bg-danger";
                                if (game.OverUnder > (game.FavoriteScore + game.UnderdogScore))
                                {
                                    game.UnderCssClass = "bg-success";
                                    if (game.Locked)
                                        totalPoints += 1;
                                }
                            }
                            if (pick.PickTypeID == (int)Enums.PickTypes.OverUnderPush)
                            {
                                if (game.OverUnder != (game.FavoriteScore + game.UnderdogScore))
                                {
                                    game.UnderCssClass = "bg-danger";
                                    game.OverCssClass = "bg-danger";
                                }
                                if (game.OverUnder == (game.FavoriteScore + game.UnderdogScore))
                                {
                                    game.OverCssClass = "bg-success";
                                    game.UnderCssClass = "bg-success";
                                    if (game.Locked)
                                        totalPoints += 2;
                                }
                            }
                        }
                        schedule.Add(game);
                    }
                }
                else
                {
                    userPicks.WeekNumber = (int)id;
                    var myPicks = pickArchives.Where(x => x.UserID == user.Id && x.WeekNumber == id).ToList();
                    foreach (var pick in myPicks)
                    {
                        var game = new ScheduleModel();
                        var currentGame = gameArchive.FirstOrDefault(x => x.GameID == pick.GameID);
                        game.GameID = pick.GameID;
                        game.GameProgress = "FINAL";
                        game.FavoriteScore = currentGame.FavoriteScore;
                        game.UnderdogScore = currentGame.UnderdogScore;
                        game.OverUnder = currentGame.OverUnder;
                        game.PointSpread = currentGame.PointSpread;
                        game.Favorite = teams.FirstOrDefault(x => x.TeamID == currentGame.FavoriteID).Abbreviation;
                        game.UnderDog = teams.FirstOrDefault(x => x.TeamID == currentGame.UnderDogID).Abbreviation;
                        if (pick.PickTypeID == (int)Enums.PickTypes.Favorite || pick.PickTypeID == (int)Enums.PickTypes.Underdog || pick.PickTypeID == (int)Enums.PickTypes.SpreadPush)
                            game.SpreadPickType = pick.PickTypeID;
                        if (pick.PickTypeID == (int)Enums.PickTypes.Over || pick.PickTypeID == (int)Enums.PickTypes.Under || pick.PickTypeID == (int)Enums.PickTypes.OverUnderPush)
                            game.OverUnderPickType = pick.PickTypeID;

                        if (game.Locked || userPicks.MyPicks)
                        {
                            if (pick.PickTypeID == (int)Enums.PickTypes.Favorite)
                            {
                                if (game.PointSpread >= (game.FavoriteScore - game.UnderdogScore))
                                    game.FavoriteCssClass = "bg-danger";
                                if (game.PointSpread < (game.FavoriteScore - game.UnderdogScore))
                                {
                                    game.FavoriteCssClass = "bg-success";
                                    if(game.Locked)
                                        totalPoints += 1;
                                }
                            }
                            if (pick.PickTypeID == (int)Enums.PickTypes.Underdog)
                            {
                                if (game.PointSpread <= (game.FavoriteScore - game.UnderdogScore))
                                    game.UnderdogCssClass = "bg-danger";
                                if (game.PointSpread > (game.FavoriteScore - game.UnderdogScore))
                                {
                                    game.UnderdogCssClass = "bg-success";
                                    if (game.Locked)
                                        totalPoints += 1;
                                }
                            }
                            if (pick.PickTypeID == (int)Enums.PickTypes.SpreadPush)
                            {
                                if (game.PointSpread != (game.FavoriteScore - game.UnderdogScore))
                                {
                                    game.FavoriteCssClass = "bg-danger";
                                    game.UnderdogCssClass = "bg-danger";
                                }
                                if (game.PointSpread == (game.FavoriteScore - game.UnderdogScore))
                                {
                                    game.FavoriteCssClass = "bg-success";
                                    game.UnderdogCssClass = "bg-success";
                                    if (game.Locked)
                                        totalPoints += 2;
                                }
                            }
                            if (pick.PickTypeID == (int)Enums.PickTypes.Over)
                            {
                                if (game.OverUnder >= (game.FavoriteScore + game.UnderdogScore))
                                    game.OverCssClass = "bg-danger";
                                if (game.OverUnder < (game.FavoriteScore + game.UnderdogScore))
                                {
                                    game.OverCssClass = "bg-success";
                                    if (game.Locked)
                                        totalPoints += 1;
                                }
                            }
                            if (pick.PickTypeID == (int)Enums.PickTypes.Under)
                            {
                                if (game.OverUnder <= (game.FavoriteScore + game.UnderdogScore))
                                    game.UnderCssClass = "bg-danger";
                                if (game.OverUnder > (game.FavoriteScore + game.UnderdogScore))
                                {
                                    game.UnderCssClass = "bg-success";
                                    if (game.Locked)
                                        totalPoints += 1;
                                }
                            }
                            if (pick.PickTypeID == (int)Enums.PickTypes.OverUnderPush)
                            {
                                if (game.OverUnder != (game.FavoriteScore + game.UnderdogScore))
                                {
                                    game.UnderCssClass = "bg-danger";
                                    game.OverCssClass = "bg-danger";
                                }
                                if (game.OverUnder == (game.FavoriteScore + game.UnderdogScore))
                                {
                                    game.OverCssClass = "bg-success";
                                    game.UnderCssClass = "bg-success";
                                    if (game.Locked)
                                        totalPoints += 2;
                                }
                            }
                        }
                        schedule.Add(game);
                    }
                }
                userPicks.TotalPoints = totalPoints;
                schedule = schedule.OrderBy(x => x.GameTime).ToList();
                userPicks.Picks = schedule;
           
                pickList.Add(userPicks);
            }

            pickList = pickList.OrderByDescending(x => x.TotalPoints).ToList();
            var rank = 1;
            var previous = 0;
            var rankCount = 0;
            foreach(var pick in pickList)
            {
                rankCount++;
                if (pick.TotalPoints == previous)
                    pick.Rank = rank;
                else
                {
                    pick.Rank = rankCount;
                    rank = rankCount;
                }
                previous = pick.TotalPoints;
            }
            return pickList;
        }

        private async Task<PickModel> GetPickInfo()
        {         

            var picks = new PickModel();
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user.PaidAmount < _config.GetValue<Double>("AppSettings:PaymentAmount") && DateTime.Now > _config.GetValue<DateTime>("AppSettings:PaymentCutoff"))
                picks.IsUserLocked = true;
            var myPicks = await _context.Picks.Where(x => x.UserID == user.Id).ToListAsync();
            picks.PickCount = myPicks.Count();
            var schedule = await GetScheduleData();
            foreach (var game in schedule)
            {
                game.FavoriteCssClass = game.UnderdogCssClass = game.SpreadPushCssClass = game.OverCssClass = game.UnderCssClass = game.OverUnderPushCssClass = "";
                var existingPick = myPicks.Where(x => x.GameID == game.GameID).ToList();
                foreach (var ep in existingPick)
                {
                    if (ep.PickTypeID == (int)Enums.PickTypes.Favorite)
                        game.FavoriteCssClass = "bg-success spread-" + game.GameID;
                    if (ep.PickTypeID == (int)Enums.PickTypes.Underdog)
                        game.UnderdogCssClass = "bg-success spread-" + game.GameID;
                    if (ep.PickTypeID == (int)Enums.PickTypes.SpreadPush)
                        game.SpreadPushCssClass = "bg-success spread-" + game.GameID;
                    if (ep.PickTypeID == (int)Enums.PickTypes.Over)
                        game.OverCssClass = "bg-success overunder-" + game.GameID;
                    if (ep.PickTypeID == (int)Enums.PickTypes.Under)
                        game.UnderCssClass = "bg-success overunder-" + game.GameID;
                    if (ep.PickTypeID == (int)Enums.PickTypes.OverUnderPush)
                        game.OverUnderPushCssClass = "bg-success overunder-" + game.GameID;
                }
            }
            picks.Schedule = schedule;
            return picks;
        }

        private async Task<List<ScheduleModel>> GetScheduleData()
        {
            await LockGames();
            await GetScores();
            var schedule = new List<ScheduleModel>();
            var games = await _context.CurrentWeeks.ToListAsync();
            foreach (var game in games)
            {
                var s = _mapper.Map<ScheduleModel>(game);
                var favorite = await _context.Teams.Where(x => x.TeamID == game.FavoriteID).FirstOrDefaultAsync();
                var underdog = await _context.Teams.Where(x => x.TeamID == game.UnderDogID).FirstOrDefaultAsync();
                s.FavortieRank = favorite.Rank;
                s.UnderdogRank = underdog.Rank;

                var actualSpread = game.FavoriteScore - game.UnderdogScore;
                var actualTotal = game.FavoriteScore + game.UnderdogScore;
                if (game.Locked)
                {
                    if (actualSpread < game.PointSpread)
                        s.UnderdogCssClass = "bg-success";
                    if (actualSpread > game.PointSpread)
                        s.FavoriteCssClass = "bg-success";
                    if(actualSpread == game.PointSpread)
                    {
                        s.UnderdogCssClass = "bg-info";
                        s.FavoriteCssClass = "bg-info";
                    }
                    if (actualTotal < game.OverUnder)
                        s.UnderCssClass = "bg-success";
                    if (actualTotal > game.OverUnder)
                        s.OverCssClass = "bg-success";
                    if (actualTotal == game.OverUnder)
                    {
                        s.UnderCssClass = "bg-info";
                        s.OverCssClass = "bg-info";
                    }
                }
                if (!game.Locked)
                {
                    var time = await ConvertTimeZone(game.GameTime);
                    s.GameProgress = time.ToString("g");
                }

                if (game.HomeTeam == game.FavoriteID)
                {
                    s.Favorite = favorite.EspnName.ToUpper();
                    s.UnderDog = underdog.EspnName;
                    s.FavoriteAbbreviation = favorite.Abbreviation.ToUpper();
                    s.UnderdogAbbreviation = underdog.Abbreviation;
                }
                else
                {
                    s.Favorite = favorite.EspnName;
                    s.UnderDog = underdog.EspnName.ToUpper();
                    s.FavoriteAbbreviation = favorite.Abbreviation;
                    s.UnderdogAbbreviation = underdog.Abbreviation.ToUpper();
                }
                //Random random = new Random();
                //s.FavoriteScore = random.Next();
                //s.UnderdogScore = random.Next();
                schedule.Add(s);
            }
            return schedule.OrderBy(x => x.GameTime).ToList();
        }
        private async Task<bool> LockGames()
        {
            DateTime now = DateTime.Now;
            var games = await _context.CurrentWeeks.ToListAsync();
            foreach (var game in games)
            {
                game.Locked = false;
                TimeZoneInfo centralZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                DateTime gameTime = TimeZoneInfo.ConvertTimeToUtc(game.GameTime, centralZone);
                DateTime currentTime = TimeZoneInfo.ConvertTimeToUtc(now, TimeZoneInfo.Local);
                if (gameTime < currentTime)
                    game.Locked = true;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> GetScores()
        {
            var url = "http://www.espn.com/ncf/bottomline/scores";

            var textFromFile = (new WebClient()).DownloadString(url);
            string[] stringSeparators = new string[] { "ncf_s_left" };

            var games = textFromFile.Split(stringSeparators, StringSplitOptions.None);

            for (var i = 1; i < games.Count(); i++)
            {
                var split1 = games[i].Split('=');
                stringSeparators = new string[] { "&ncf_s_right" };
                var split2 = split1[1].Split(stringSeparators, StringSplitOptions.None);
                stringSeparators = new string[] { "%20" };
                var split3 = split2[0].Split(stringSeparators, StringSplitOptions.None);
                var scoreUrl = "";
                if (split1.Length > 4)
                {
                    scoreUrl = split1[3] + "=" + split1[4];
                    scoreUrl = scoreUrl.Replace("http://espn.go.com/ncf/scoring", "http://www.espn.com/college-football/game");
                }
                int awayScore = 0;
                int homeScore = 0;
                string awayTeam = string.Empty;
                string homeTeam = string.Empty;
                string period = string.Empty;
                int? homerank = null;
                int? awayrank = null;
                bool awayProcessed = false;
                bool homeProcessed = false;
                foreach (var ss in split3)
                {
                    var s = ss.Trim('^').Replace("(OH)", "OH");
                    int score = 0;
                    if ((!int.TryParse(s, out score) && s != "at") || (awayProcessed && homeProcessed))
                    {
                        if (s != string.Empty)
                        {
                            if (s.ToCharArray()[0] == '(' && awayProcessed && homeTeam.Length > 0)
                            {
                                homeProcessed = true;
                            }
                            if (awayProcessed && homeProcessed)
                            {
                                period += s.TrimStart('(').TrimEnd(')') + " ";
                            }
                            else if (!awayProcessed)
                            {
                                awayTeam += s + " ";
                            }
                            else
                            {
                                homeTeam += s + " ";
                            }
                        }
                    }
                    else if (int.TryParse(s, out score))
                    {
                        if (!awayProcessed)
                        {
                            awayScore = score;
                            awayProcessed = true;
                        }
                        else
                        {
                            homeScore = score;
                            homeProcessed = true;
                        }
                    }
                    else
                    {
                        awayProcessed = true;
                    }
                }
                if (homeTeam != string.Empty && homeTeam.ToCharArray()[0] == '(')
                {
                    var splitIt = homeTeam.Split(')');
                    homeTeam = splitIt[1];
                    homerank = Convert.ToInt16(splitIt[0].TrimStart('('));
                }
                if (awayTeam != string.Empty && awayTeam.ToCharArray()[0] == '(')
                {
                    var splitIt = awayTeam.Split(')');
                    awayTeam = splitIt[1];
                    awayrank = Convert.ToInt16(splitIt[0].TrimStart('('));
                }
                homeTeam = homeTeam.Replace("%26", "&").Trim().Trim('^').Replace("'", "");
                awayTeam = awayTeam.Replace("%26", "&").Trim().Trim('^').Replace("'", "");
                byte[] tempBytes;
                tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(homeTeam);
                homeTeam = System.Text.Encoding.UTF8.GetString(tempBytes);
                tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(awayTeam);
                awayTeam = System.Text.Encoding.UTF8.GetString(tempBytes);

                homeTeam = homeTeam.Replace('?', 'e');
                awayTeam = awayTeam.Replace('?', 'e');

                var homeCtx = await _context.Teams.FirstOrDefaultAsync(x => x.EspnName.ToLower() == homeTeam.ToLower());
                var awayCtx = await _context.Teams.FirstOrDefaultAsync(x => x.EspnName.ToLower() == awayTeam.ToLower());
                if (homeCtx != null)
                {
                    homeCtx.Rank = homerank;
                }
                if (awayCtx != null)
                {
                    awayCtx.Rank = awayrank;
                }
                if (homeCtx != null && awayCtx != null)
                {
                    var game = await _context.CurrentWeeks.FirstOrDefaultAsync(x => x.FavoriteID == homeCtx.TeamID && x.UnderDogID == awayCtx.TeamID);
                    if (game == null)
                    {
                        game = await _context.CurrentWeeks.FirstOrDefaultAsync(x => x.UnderDogID == homeCtx.TeamID && x.FavoriteID == awayCtx.TeamID);

                    }
                    if (game != null)
                    {
                        if (game.FavoriteID == homeCtx.TeamID)
                        {
                            game.FavoriteScore = homeScore;
                            game.UnderdogScore = awayScore;
                        }
                        if (game.FavoriteID == awayCtx.TeamID)
                        {
                            game.FavoriteScore = awayScore;
                            game.UnderdogScore = homeScore;
                        }
                        game.GameUrl = scoreUrl;
                        game.GameProgress = period.Trim();
                        if (period.Contains("FINAL"))
                        {
                            var actualSpread = game.FavoriteScore - game.UnderdogScore;
                            var totelScore = game.FavoriteScore + game.UnderdogScore;

                            var picks = await _context.Picks.Where(x => x.GameID == game.GameID).ToListAsync();
                            foreach (var pick in picks)
                            {
                                if (actualSpread > game.PointSpread && pick.PickTypeID == (int)Enums.PickTypes.Favorite)
                                    pick.CorrectPoints = 1;
                                else if (actualSpread < game.PointSpread && pick.PickTypeID == (int)Enums.PickTypes.Underdog)
                                    pick.CorrectPoints = 1;
                                else if (totelScore > game.OverUnder && pick.PickTypeID == (int)Enums.PickTypes.Over)
                                    pick.CorrectPoints = 1;
                                else if (totelScore < game.OverUnder && pick.PickTypeID == (int)Enums.PickTypes.Under)
                                    pick.CorrectPoints = 1;
                                else if (actualSpread == game.PointSpread && pick.PickTypeID == (int)Enums.PickTypes.SpreadPush)
                                    pick.CorrectPoints = 2;
                                else if (totelScore == game.OverUnder && pick.PickTypeID == (int)Enums.PickTypes.OverUnderPush)
                                    pick.CorrectPoints = 2;
                                else
                                    pick.CorrectPoints = 0;
                            }
                        }
                        await _context.SaveChangesAsync();
                    }
                }
            }

            return true;
        }

        private async Task<DateTime> ConvertTimeZone(DateTime dateToConvert)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                var dateToReturn = TimeZoneInfo.ConvertTimeToUtc(dateToConvert, timeZone);
                timeZone = TimeZoneInfo.FindSystemTimeZoneById(user.TimeZone);
                return TimeZoneInfo.ConvertTimeFromUtc(dateToReturn, timeZone);
            }
            return dateToConvert;
            
        }

    }
}
