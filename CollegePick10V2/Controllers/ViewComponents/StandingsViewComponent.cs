using CollegePick10V2.Entities;
using CollegePick10V2.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CollegePick10V2.Controllers
{
    public class StandingsViewComponent : ViewComponent
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public StandingsViewComponent(ApplicationContext context, UserManager<User> userManager, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await GetStandings());
        }

        private async Task<List<StandingsModel>> GetStandings()
        {
            var standings = new List<StandingsModel>();
            var users = await _context.Users.ToListAsync();
            var lockedGames = await _context.CurrentWeeks.Where(x => x.Locked).Select(x => x.GameID).ToListAsync();
            var currentWeek = await _context.Picks.Where(x => lockedGames.Contains(x.GameID)).ToListAsync();
            var pastPicks = await _context.PickArchives.ToListAsync();
            foreach (var user in users)
            {
                var standing = new StandingsModel();
                standing.Name = user.FirstName + " " + user.LastName;
                standing.CurrentWeek = currentWeek.Where(x => x.UserID == user.Id).Sum(x => x.CorrectPoints);
                standing.SeasonTotal = standing.CurrentWeek + pastPicks.Where(x => x.UserID == user.Id).Sum(x => x.CorrectPoints);
                standing.Balance = GetMoney(user, pastPicks, users.Count());
                standings.Add(standing);
            }
            var orderedStandings = standings.OrderByDescending(x => x.SeasonTotal);
            var rank = 1;
            var rankcount = 0;
            var previous = 0;
            foreach(var standing in orderedStandings)
            {
                rankcount++;
                if (standing.SeasonTotal == previous)
                    standing.Rank = rank;
                else
                {
                    standing.Rank = rankcount;
                    rank = rankcount;
                }
                previous = standing.SeasonTotal;
            }
            return orderedStandings.ToList();
        }

        private string GetMoney(User user, List<PickArchive> pickArchives, int userCount)
        {
            var balance = (double)user.PaidAmount;
            var week = pickArchives.Max(x => x.WeekNumber);
            if (week == null) week = 0;

            for(var i = 1; i <= week; i++)
            {
                balance = balance - _config.GetValue<Double>("AppSettings:PaymentAmount");
                var highest = pickArchives.Where(x => x.WeekNumber == i).GroupBy(x => x.UserID).Select(x => new { UserID = x.Key, Correct = x.Sum(y => y.CorrectPoints) }).ToList();
                var high = 0;
                if (highest.Count() > 0)
                    high = highest.Max(x => x.Correct);
                var tie = highest.Where(x => x.Correct == high).Count();
                var totalPoints = pickArchives.Where(x => x.UserID == user.Id && x.WeekNumber == i).Sum(x => x.CorrectPoints);
                var pool = (_config.GetValue<Double>("AppSettings:PaymentAmount")/2)*userCount;

                if (totalPoints == high && tie > 0)
                    balance = balance + (pool / tie);
            }
            return balance.ToString("C");
        }
    }
}
