using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Dal.Admin.Mappings;
using TechBlog.Dal.Cache;
using TechBlog.Dal.Share.Mappings;
using TechBlog.Dal.Share.Repositories;
using TechBlog.Interfaces.Dal.Share;

namespace TechBlog.Dependecies.Installers
{
    public class DalShareInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Repositories
            container.
                Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestylePerWebRequest());

            // Cache
            container.Register(Component.For<ICacheManager>().ImplementedBy<CacheManager>().LifestyleSingleton());

            // Mapper
            container.Register(Component.For<IMapper>().UsingFactoryMethod(x =>
            {
                return new MapperConfiguration(c =>
                {
                    c.AddProfile<DalShareMapperProfile>();
                    c.AddProfile<DalAdminMapperProfile>();
                }).CreateMapper();
            }));
        }
    }
}
