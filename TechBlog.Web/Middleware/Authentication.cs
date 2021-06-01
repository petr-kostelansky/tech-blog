using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TechBlog.Web.Common.Providers;

namespace TechBlog.Web.Middleware
{
    public class Authentication
    {
        public static void Configure(IAppBuilder app)
        {
            //app.CreatePerOwinContext<ApplicationUserManager>(() => ApplicationUserManager.Create(app));
            //app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Admin/Account/Login"),
                Provider = new MyCookieAuthenticationProvider()
            });
        }
    }

    internal class MyCookieAuthenticationProvider : CookieAuthenticationProvider
    {
        public override Task ValidateIdentity(CookieValidateIdentityContext context)
        {
            return base.ValidateIdentity(context);
        }

        public override void ResponseSignIn(CookieResponseSignInContext context)
        {
            base.ResponseSignIn(context);
        }
    }
}