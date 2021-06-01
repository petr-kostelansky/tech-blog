using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBlog.Contract.Models.Users
{
    public class User : UserInfo, IUser<int>
    {
        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public int AccessFailedCount { get; set; }
    }
}
