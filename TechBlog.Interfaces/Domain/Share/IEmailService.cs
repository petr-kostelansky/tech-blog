using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBlog.Interfaces.Domain.Share
{
    public interface IEmailService
    {
        void SendEmail(IEnumerable<string> emails, string subject, string body);
    }
}
