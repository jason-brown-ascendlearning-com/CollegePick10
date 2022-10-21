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
    public class SharedViewComponent : ViewComponent
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public SharedViewComponent(ApplicationContext context, UserManager<User> userManager, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await GetPaymentStatus());
        }

        private async Task<PaymentStatusModel> GetPaymentStatus()
        {
            var paymentStatus = new PaymentStatusModel();
            paymentStatus.IsLoggedIn = User.Identity.IsAuthenticated;
            if (!paymentStatus.IsLoggedIn) return new PaymentStatusModel();
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var paymentAmount = _config.GetValue<Double>("AppSettings:PaymentAmount")*_config.GetValue<Double>("AppSettings:TotalWeeks");
            paymentStatus.HasPaid = user.PaidAmount >= paymentAmount;
            paymentStatus.PaymentDeadline = _config.GetValue<string>("AppSettings:PaymentCutoff");
            return paymentStatus;
        }

        
    }
}
