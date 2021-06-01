using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Interfaces.Domain.Share;

namespace TechBlog.Domain.Share.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(IEnumerable<string> emails, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mailMessage = new MailMessage();

            foreach (var email in emails)
            {
                mailMessage.To.Add(email);
            }

            mailMessage.Subject = subject;
            mailMessage.Body = body;

            smtpClient.Send(mailMessage);
        }
    }
}
