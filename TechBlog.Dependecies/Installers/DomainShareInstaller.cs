using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Domain.Share.Services;
using TechBlog.Interfaces.Domain.Share;

namespace TechBlog.Dependecies.Installers
{
    public class DomainShareInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.
                Register(Component.For<IEmailService>().ImplementedBy<EmailService>().LifestylePerWebRequest());
        }
    }
}
