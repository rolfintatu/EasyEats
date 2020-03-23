using System;
using System.Net;
using System.Net.Mail;
using Application.Common.Dtos;
using Application.Common.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService, IEmailSender
    {
        private readonly IConfiguration configuration;
        public EmailService(IConfiguration configuration)
            => (this.configuration) = (configuration);

        public async Task Send(MessageDto message)
        {
            using(var client = new SmtpClient())
            {
                var credentials = new NetworkCredential()
                {
                    UserName = configuration["EmailSender:email"],
                    Password = configuration["EmailSender:password"]
                };

                client.Host = configuration["EmailSender:host"];
                client.Port = int.Parse(configuration["EmailSender:port"]);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;

                using (var EmailMessage = new MailMessage())
                {
                    EmailMessage.To.Add(message.To);
                    EmailMessage.From = new MailAddress(configuration["EmailSender:email"]);
                    EmailMessage.Subject = message.Subject;
                    EmailMessage.Body = message.HtmlBody;

                    client.Send(EmailMessage);
                }
            }
            await Task.CompletedTask;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (var client = new SmtpClient())
            {
                var credentials = new NetworkCredential()
                {
                    UserName = configuration["EmailSender:email"],
                    Password = configuration["EmailSender:password"]
                };

                client.Host = configuration["EmailSender:host"];
                client.Port = int.Parse(configuration["EmailSender:port"]);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;

                using (var EmailMessage = new MailMessage())
                {
                    EmailMessage.To.Add(email);
                    EmailMessage.From = new MailAddress(configuration["EmailSender:email"]);
                    EmailMessage.Subject = subject;
                    EmailMessage.Body = htmlMessage;

                    client.Send(EmailMessage);
                }
            }
            await Task.CompletedTask;
        }
    }
}
