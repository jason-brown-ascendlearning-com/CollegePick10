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
    public class PayoutViewComponent : ViewComponent
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public PayoutViewComponent(ApplicationContext context, UserManager<User> userManager, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await GetPayouts());
        }

        private async Task<PayoutModel> GetPayouts()
        {
            var payouts = new PayoutModel();
            var usercount = await _context.Users.CountAsync();
            var totalPayout = ((_config.GetValue<Double>("AppSettings:PaymentAmount") / 2 * _config.GetValue<Double>("AppSettings:TotalWeeks")) - _config.GetValue<Double>("AppSettings:PayPalFee")) * usercount;
            var weeklyPayout = (_config.GetValue<Double>("AppSettings:PaymentAmount") / 2) * usercount;
            payouts.SeasonTotal = totalPayout.ToString("C");
            payouts.WeeklyTotal = weeklyPayout.ToString("C");
            payouts.Payouts = new List<PaymentModel>();
            if(usercount < 13)
            {
                payouts.Payouts.Add(new PaymentModel { Label = "1st Place", Amount = (totalPayout * .50).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "2nd Place", Amount = (totalPayout * .30).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "3rd Place", Amount = (totalPayout * .20).ToString("C") });
            }
            else if(usercount < 21)
            {
                payouts.Payouts.Add(new PaymentModel { Label = "1st Place", Amount = (totalPayout * .45).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "2nd Place", Amount = (totalPayout * .30).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "3rd Place", Amount = (totalPayout * .15).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "4th Place", Amount = (totalPayout * .10).ToString("C") });
            }
            else if (usercount < 31)
            {
                payouts.Payouts.Add(new PaymentModel { Label = "1st Place", Amount = (totalPayout * .40).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "2nd Place", Amount = (totalPayout * .27).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "3rd Place", Amount = (totalPayout * .15).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "4th Place", Amount = (totalPayout * .10).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "5th Place", Amount = (totalPayout * .08).ToString("C") });
            }
            else if (usercount < 41)
            {
                payouts.Payouts.Add(new PaymentModel { Label = "1st Place", Amount = (totalPayout * .37).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "2nd Place", Amount = (totalPayout * .25).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "3rd Place", Amount = (totalPayout * .15).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "4th Place", Amount = (totalPayout * .10).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "5th Place", Amount = (totalPayout * .075).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "6th Place", Amount = (totalPayout * .055).ToString("C") });
            }
            else if (usercount < 51)
            {
                payouts.Payouts.Add(new PaymentModel { Label = "1st Place", Amount = (totalPayout * .35).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "2nd Place", Amount = (totalPayout * .225).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "3rd Place", Amount = (totalPayout * .15).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "4th Place", Amount = (totalPayout * .10).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "5th Place", Amount = (totalPayout * .075).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "6th Place", Amount = (totalPayout * .055).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "7th Place", Amount = (totalPayout * .045).ToString("C") });
            }
            else if (usercount < 61)
            {
                payouts.Payouts.Add(new PaymentModel { Label = "1st Place", Amount = (totalPayout * .32).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "2nd Place", Amount = (totalPayout * .21).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "3rd Place", Amount = (totalPayout * .15).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "4th Place", Amount = (totalPayout * .101).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "5th Place", Amount = (totalPayout * .076).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "6th Place", Amount = (totalPayout * .056).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "7th Place", Amount = (totalPayout * .046).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "8th Place", Amount = (totalPayout * .041).ToString("C") });
            }
            else if (usercount < 81)
            {
                payouts.Payouts.Add(new PaymentModel { Label = "1st Place", Amount = (totalPayout * .30).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "2nd Place", Amount = (totalPayout * .20).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "3rd Place", Amount = (totalPayout * .15).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "4th Place", Amount = (totalPayout * .10).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "5th Place", Amount = (totalPayout * .075).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "6th Place", Amount = (totalPayout * .055).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "7th Place", Amount = (totalPayout * .045).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "8th Place", Amount = (totalPayout * .04).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "9th Place", Amount = (totalPayout * .035).ToString("C") });
            }
            else if (usercount < 101)
            {
                payouts.Payouts.Add(new PaymentModel { Label = "1st Place", Amount = (totalPayout * .275).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "2nd Place", Amount = (totalPayout * .185).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "3rd Place", Amount = (totalPayout * .14).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "4th Place", Amount = (totalPayout * .095).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "5th Place", Amount = (totalPayout * .07).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "6th Place", Amount = (totalPayout * .0525).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "7th Place", Amount = (totalPayout * .0425).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "8th Place", Amount = (totalPayout * .035).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "9th Place", Amount = (totalPayout * .03).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "10th-12th Place", Amount = (totalPayout * .025).ToString("C") });
            }
            else if (usercount < 121)
            {
                payouts.Payouts.Add(new PaymentModel { Label = "1st Place", Amount = (totalPayout * .25).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "2nd Place", Amount = (totalPayout * .17).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "3rd Place", Amount = (totalPayout * .13).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "4th Place", Amount = (totalPayout * .0925).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "5th Place", Amount = (totalPayout * .065).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "6th Place", Amount = (totalPayout * .0525).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "7th Place", Amount = (totalPayout * .0425).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "8th Place", Amount = (totalPayout * .035).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "9th Place", Amount = (totalPayout * .0275).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "10th-12th Place", Amount = (totalPayout * .024).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "13th-15th Place", Amount = (totalPayout * .021).ToString("C") });
            }
            else if (usercount < 141)
            {
                payouts.Payouts.Add(new PaymentModel { Label = "1st Place", Amount = (totalPayout * .23).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "2nd Place", Amount = (totalPayout * .16).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "3rd Place", Amount = (totalPayout * .1215).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "4th Place", Amount = (totalPayout * .09).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "5th Place", Amount = (totalPayout * .06).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "6th Place", Amount = (totalPayout * .05).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "7th Place", Amount = (totalPayout * .04).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "8th Place", Amount = (totalPayout * .03).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "9th Place", Amount = (totalPayout * .025).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "10th-12th Place", Amount = (totalPayout * .0225).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "13th-15th Place", Amount = (totalPayout * .0215).ToString("C") });
                payouts.Payouts.Add(new PaymentModel { Label = "16th-18th Place", Amount = (totalPayout * .0205).ToString("C") });
            }
            return payouts;
        }
    }
}
