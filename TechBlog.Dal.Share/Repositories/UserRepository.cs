using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = TechBlog.Contract.Models.Users;
using TechBlog.DbContext;
using TechBlog.Interfaces.Dal.Share;
using AutoMapper;

namespace TechBlog.Dal.Share.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly IMapper mapper;

        public UserRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<Model.User> FindUserByIdAsync(int userId)
        {
            var user = await Task.FromResult(Context.ViewUsers.FirstOrDefault(s => s.Id == userId));
            var res = mapper.Map<ViewUser, Model.User>(user);

            return res;
        }

        public async Task<Model.User> FindUserByNameAsync(string userName)
        {
            var user = await Task.FromResult(Context.ViewUsers.FirstOrDefault(s => s.UserName == userName));
            var res = mapper.Map<ViewUser, Model.User>(user);

            return res;
        }

        public async Task<Model.User> FindUserByEmailAsync(string email)
        {
            var user = await Task.FromResult(Context.ViewUsers.FirstOrDefault(s => s.Email == email));
            var res = mapper.Map<ViewUser, Model.User>(user);

            return res;
        }
    }
}
