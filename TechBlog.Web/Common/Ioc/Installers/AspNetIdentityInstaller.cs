using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechBlog.Contract.Models.Users;
using TechBlog.Domain.Share.Services;
using TechBlog.Web.Common.Providers;

namespace TechBlog.Web.Common.Ioc.Installers
{
    public class AspNetIdentityInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.
                Register(Component.For<IUserStore<User, int>, IUserEmailStore<User, int>, IUserPasswordStore<User, int>>().ImplementedBy<UserService>().LifestyleTransient()).

                Register(Component.For<ApplicationUserManager>().UsingFactoryMethod(kernel =>
                                                                    ApplicationUserManager.Create(
                                                                        kernel.Resolve<IUserStore<User, int>>(),
                                                                        kernel.Resolve<IAppBuilder>()))
                                                                .LifestyleTransient()).

                Register(Component.For<IAuthenticationManager>().UsingFactoryMethod(kernel => 
                                                                    HttpContext.Current.GetOwinContext().Authentication)
                                                                .LifestyleTransient()).

                Register(Component.For<ApplicationSignInManager>().LifestyleTransient());
        }
    }
}