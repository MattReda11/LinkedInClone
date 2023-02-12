using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace LinkedInClone.Services
{
    public class EmailSender : IEmailSender
    {
        public readonly AuthMessageSenderOptions _options;

        public EmailSender(IOptions<AuthMessageSenderOptions> options)
        {
            _options = options.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(email, subject, message);
        }

        private Task Execute(string email, string subject, string message)
        {
            using (var client = new SmtpClient())
            {
                client.Host = _options.SmtpServer;
                client.Port = _options.SmtpPort;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_options.SenderEmail, _options.SenderPassword);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_options.SenderEmail),
                    To = { email },
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                return client.SendMailAsync(mailMessage);
            }
        }

    }
}