using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Dal.CacheDecorator;
using TechBlog.Dal.Repositories;
using TechBlog.Interfaces.Dal.Web;

namespace TechBlog.Dependecies.Installers
{
    public class DalWebInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.
                Register(Component.For<IArticleRepository>().ImplementedBy<ArticleCache>().LifestylePerWebRequest()).
                Register(Component.For<IArticleRepository>().ImplementedBy<ArticleRepository>().LifestylePerWebRequest()).
                Register(Component.For<ISitemapRepository>().ImplementedBy<SitemapRepository>().LifestylePerWebRequest()).
                Register(Component.For<ITagRepository>().ImplementedBy<TagRepository>().LifestylePerWebRequest());
        }
    }
}
