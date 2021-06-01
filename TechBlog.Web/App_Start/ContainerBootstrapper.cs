using System;
using Castle.Windsor;
using Castle.Windsor.Installer;
using TechBlog.Web.Common.Ioc.Installers;
using TechBlog.Dependecies.Installers;
using Microsoft.AspNet.Identity;
using TechBlog.Contract.Models.Users;

namespace TechBlog.Web.App_Start
{
    public class ContainerBootstrapper : IContainerAccessor, IDisposable
    {
        readonly IWindsorContainer container;

        ContainerBootstrapper(IWindsorContainer container)
        {
            this.container = container;
        }

        public IWindsorContainer Container
        {
            get { return container; }
        }

        public static ContainerBootstrapper Bootstrap()
        {
            // MVC dependencies
            var container = new WindsorContainer().
                Install(new CustomInstaller()).
                Install(new ControllersInstaller()).
                Install(new ManagersInstaller()).
                Install(new AdminManagersInstaller());

            // Domain
            container.
                Install(new DomainWebInstaller()).
                Install(new DomainAdminInstaller()).
                Install(new DomainShareInstaller());

            // Dal
            container.
                Install(new DalWebInstaller()).
                Install(new DalAdminInstaller()).
                Install(new DalShareInstaller());

            //AspNetIdentity
            container.
                Install(new AspNetIdentityInstaller());

            return new ContainerBootstrapper(container);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}