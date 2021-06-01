using Castle.Windsor;
using System;
using WebActivatorEx;

//[assembly: PreApplicationStartMethod(typeof(TechBlog.Web.App_Start.WindsorActivator), "PreStart")]
//[assembly: ApplicationShutdownMethodAttribute(typeof(TechBlog.Web.App_Start.WindsorActivator), "Shutdown")]

namespace TechBlog.Web.App_Start
{
    public static class WindsorActivator
    {
        static ContainerBootstrapper bootstrapper;

        public static void PreStart()
        {
            bootstrapper = ContainerBootstrapper.Bootstrap();
        }

        public static IWindsorContainer Container
        {
            get { return bootstrapper.Container; }
        }

        public static void Shutdown()
        {
            if (bootstrapper != null)
                bootstrapper.Dispose();
        }
    }
}