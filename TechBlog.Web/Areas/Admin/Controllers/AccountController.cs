using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TechBlog.Domain.Share.Services;
using TechBlog.Interfaces.Domain.Share;
using TechBlog.Web.Areas.Admin.ViewModels;
using TechBlog.Web.Areas.Admin.ViewModels.Account;
using TechBlog.Web.Common;
using TechBlog.Web.Common.Providers;

namespace TechBlog.Web.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private readonly Lazy<IEmailService> emailService;
        private readonly Lazy<IAuthenticationManager> authenticationManager;
        private readonly Lazy<ApplicationUserManager> userManager;
        private readonly Lazy<ApplicationSignInManager> signInManager;

        public AccountController(Lazy<IAuthenticationManager> authenticationManager, 
            Lazy<ApplicationUserManager> userManager,
            Lazy<ApplicationSignInManager> signInManager,
            Lazy<IEmailService> emailService)
        {
            this.emailService = emailService;
            this.authenticationManager = authenticationManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("login");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel viewModel)
        {
            string errors = null;
            bool reCaptchaIsValid = ReCaptcha.ValidateCaptcha(Request["g-recaptcha-response"], out errors);

            List<string> to = new List<string> { "email@domain.com" };

            if (reCaptchaIsValid)
            {
                if (viewModel != null && viewModel.Login != null)
                {
                    var user = await userManager.Value.FindByEmailAsync(viewModel.Login.Email); // viewModel.Login.Password);
                    if (user != null)
                    {
                        var verified = await userManager.Value.CheckPasswordAsync(user, viewModel.Login.Password);
                        if (verified)
                        {
                            await signInManager.Value.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                            return RedirectToAction("Index", "Article", new { area = "Admin" });
                        }
                        else
                        {
                            emailService.Value.SendEmail(to, "LoginFailed", "email: " + viewModel.Login.Email);
                        }
                    }
                }
            }
            else
            {
                emailService.Value.SendEmail(to, "LoginFailed", "reCaptcha is not valid");
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            authenticationManager.Value.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous]
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
