using AutoMapper;
using CollegePick10V2.Entities;
using CollegePick10V2.Models;
using EmailService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CollegePick10V2.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;

        public AccountController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, IConfiguration config)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _config = config;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var profile = new UserProfileModel();
            profile.Email = user.Email;
            profile.FirstName = user.FirstName;
            profile.LastName = user.LastName;
            profile.TimeZone = user.TimeZone;
            profile.PhoneNumber = user.PhoneNumber;
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UserProfileModel userModel)
        {
            if (!ModelState.IsValid)
                return View(userModel);


            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            user.Email = userModel.Email;
            user.UserName = userModel.Email;
            user.NormalizedUserName = userModel.Email.ToUpper();
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.PhoneNumber = userModel.PhoneNumber;
            user.TimeZone = userModel.TimeZone;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(userModel);
            }
            return RedirectToAction(nameof(AccountController.Profile), "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Register(UserRegistrationModel userModel)
        {
            if(!ModelState.IsValid)
                return View(userModel);

            if(userModel.InviteCode != _config.GetValue<string>("AppSettings:InviteCode"))
            {
                ModelState.AddModelError("", "Invalid Registration Code");
                return View();
            }
            var user = _mapper.Map<User>(userModel);

            var result = await _userManager.CreateAsync(user, userModel.Password);
            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(userModel);
            }
            await _userManager.AddToRoleAsync(user, "Member");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel userModel, string returnUrl = null)
        {
            if(!ModelState.IsValid)
                return View();

            var result = await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, userModel.RememberMe, false);
            if(result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(forgotPasswordModel);
            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
            var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
            await _emailSender.SendEmailAsync(message);
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);
            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
                RedirectToAction(nameof(ResetPasswordConfirmation));
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
