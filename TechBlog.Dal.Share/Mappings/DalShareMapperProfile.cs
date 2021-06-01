using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = TechBlog.Contract.Models.Users;
using TechBlog.DbContext;

namespace TechBlog.Dal.Share.Mappings
{
    public class DalShareMapperProfile : Profile
    {
        public DalShareMapperProfile()
        {
            CreateMap<ViewUser, Model.User>();       
        }
    }
}
