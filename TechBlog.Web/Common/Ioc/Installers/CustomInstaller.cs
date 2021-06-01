using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechBlog.Interfaces;

namespace TechBlog.Web.Common.Ioc.Installers
{
    public class CustomInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ILazyComponentLoader>().
                ImplementedBy<LazyOfTComponentLoader>());

            container.Register(Component.For<IHttpContext>().ImplementedBy<Web.Common.MyHttpContext>().LifestylePerWebRequest());
        }
    }
}