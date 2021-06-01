using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Users;
using TechBlog.Interfaces.Dal.Share;

namespace TechBlog.Domain.Share.Services
{
    public class UserService : IUserStore<User, int>,
                               IUserEmailStore<User, int>,
                               IUserPasswordStore<User, int>
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        #region IUserStore
        public Task CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return userRepository.FindUserByIdAsync(userId);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return userRepository.FindUserByNameAsync(userName);
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IUserEmailStore
        public Task<User> FindByEmailAsync(string email)
        {
            return userRepository.FindUserByEmailAsync(email);
        }

        public Task<string> GetEmailAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(User user, string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IUserPasswordStore
        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            var pass = Task.FromResult<string>(user.PasswordHash);
            return pass;
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
