using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechBlog.Web.Areas.Admin.Managers;
using TechBlog.Web.Areas.Admin.Managers.Interfaces;

namespace TechBlog.Web.Common.Ioc.Installers
{
    public class AdminManagersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.
                Register(Component.For<IArticleManager>().ImplementedBy<ArticleManager>().LifestyleTransient()).
                Register(Component.For<ITagManager>().ImplementedBy<TagManager>().LifestyleTransient());
            //    Register(Component.For<ITagFactory>().ImplementedBy<TagFactory>().LifestylePerWebRequest()).
            //    Register(Component.For<ITemplateFactory>().ImplementedBy<TemplateFactory>().LifestylePerWebRequest()).
            //    Register(Component.For<ICollectionPhotoFactory>().ImplementedBy<CollectionPhotoFactory>().LifestylePerWebRequest()).
            //    Register(Component.For<IPhotoCollectionFactory>().ImplementedBy<PhotoCollectionFactory>().LifestylePerWebRequest()).
            //    Register(Component.For<IImageFactory>().ImplementedBy<ImageFactory>().LifestylePerWebRequest()).
            //    Register(Component.For<ISearchFactory>().ImplementedBy<SearchFactory>().LifestylePerWebRequest());

        }
    }
}