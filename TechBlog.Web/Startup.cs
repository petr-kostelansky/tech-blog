using Microsoft.Owin;
using Owin;
using TechBlog.Web.App_Start;
using TechBlog.Web.Middleware;

[assembly: OwinStartupAttribute(typeof(TechBlog.Web.Startup))]
namespace TechBlog.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = WindsorActivator.Container;
            container.Register(Castle.MicroKernel.Registration.Component.For<IAppBuilder>().Instance(app));

            Authentication.Configure(app);


        }
    }
}