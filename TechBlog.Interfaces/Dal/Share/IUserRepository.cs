using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Users;

namespace TechBlog.Interfaces.Dal.Share
{
    public interface IUserRepository
    {
        Task<User> FindUserByIdAsync(int userId);

        Task<User> FindUserByNameAsync(string userName);

        Task<User> FindUserByEmailAsync(string email);
    }
}
