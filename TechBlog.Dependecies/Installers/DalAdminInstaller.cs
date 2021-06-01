using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Dal.Admin.Repositories;
using TechBlog.Interfaces.Dal.Admin;

namespace TechBlog.Dependecies.Installers
{
    public class DalAdminInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.
                Register(Component.For<IArticleRepository>().ImplementedBy<ArticleRepository>().LifestylePerWebRequest()).
                Register(Component.For<ITagRepository>().ImplementedBy<TagRepository>().LifestylePerWebRequest());
        }
    }
}
