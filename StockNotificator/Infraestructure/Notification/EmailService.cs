using MailKit.Net.Smtp;
using MimeKit;
using Quartz.Logging;
using StockNotificator.Application.Interfaces.Repositories;

namespace StockNotificator.Infraestructure.Notification
{
    public class EmailService(
        IConfiguration configuration,
        ILogger<EmailService> logger
        ) : IEmailService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<EmailService> _logger = logger;

        public async Task SendAsync(string to, string subject, string body)
        {
            string server = _configuration["EmailSettings:Server"]!;
            string from = _configuration["EmailSettings:From"]!;
            string password = _configuration["EmailSettings:AppPassword"]!;
            int port = int.Parse(_configuration["EmailSettings:Port"]!);

            var msg = new MimeMessage
            {
                From = { new MailboxAddress("App", from) },
                To = { new MailboxAddress("User", to) },
                Subject = subject,
                Body = new TextPart("plain") { Text = body }
            };

            try
            {
                using var smtp = new SmtpClient();

                _logger.LogInformation("Smtp send Email...");

                await smtp.ConnectAsync(server, port, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(from, password);
                await smtp.SendAsync(msg);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in SendAsync(): {0}",
                    ex.ToString());
            }
        }
    }
}
