using Microsoft.AspNet.Identity;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechBlog.Contract.Models.Users;
using TechBlog.Domain.Share.Services;
using System.Threading.Tasks;

namespace TechBlog.Web.Common.Providers
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User, int> userStore)
            : base(userStore)
        {
        }

        //protected override async Task<bool> VerifyPasswordAsync(IUserPasswordStore<User, int> store, User user, string password)
        //{
        //    string hash = await store.GetPasswordHashAsync(user);//.WithCurrentCulture<string>();

        //    var result = this.PasswordHasher.VerifyHashedPassword(hash, password) != PasswordVerificationResult.Failed;

        //    return result;
        //}

        public static ApplicationUserManager Create(IUserStore<User, int> store, IAppBuilder app)
        {
            var manager = new ApplicationUserManager(store);

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            object obj;
            string appName = String.Empty;
            if (app.Properties.TryGetValue("host.AppName", out obj))
            {
                appName = obj as string;
            }

            var protectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider(appName);
            Microsoft.Owin.Security.DataProtection.IDataProtector dataProtector = protectionProvider.Create("ASP.NET Identity");

            manager.UserTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<User, int>(dataProtector);

            return manager;
        }
    }
}