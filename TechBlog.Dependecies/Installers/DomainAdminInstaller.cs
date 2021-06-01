using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Domain.Admin.Services;
using TechBlog.Interfaces.Domain.Admin;

namespace TechBlog.Dependecies.Installers
{
    public class DomainAdminInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.
                Register(Component.For<IArticleService>().ImplementedBy<ArticleService>().LifestyleTransient()).
                Register(Component.For<ITagService>().ImplementedBy<TagService>().LifestyleTransient());
        }
    }
}
