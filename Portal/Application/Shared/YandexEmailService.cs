using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Portal.Application.Interfaces;

namespace Portal.Application.Shared
{
    public class YandexEmailService : IEmailService
    {
        private readonly IConfigurationRoot config;

        public YandexEmailService(IConfigurationRoot config)
        {
            this.config = config;
        }

        public async void SendEmailAsync((string address, string subject, string message) email)
        {
            (string login, string password) sender = ("kpitv@yandex.ua", config["kpitv@yandex.ua"]);

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("KPI TV Portal", sender.login));
            emailMessage.To.Add(new MailboxAddress(email.address));
            emailMessage.Subject = email.subject;
            emailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = email.message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 465, true);
                await client.AuthenticateAsync(sender.login, sender.password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
