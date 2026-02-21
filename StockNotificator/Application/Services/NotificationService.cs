using Quartz.Logging;
using StockNotificator.Application.Interfaces.Repositories;
using StockNotificator.Application.Interfaces.Services;

namespace StockNotificator.Application.Services
{
    public class NotificationService(
        IEmailService emailService,
        ILogger<NotificationService> logger
        ) : INotificationService
    {
        private readonly IEmailService _emailService = emailService;
        private readonly ILogger<NotificationService> _logger = logger;

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try 
            {
                _logger.LogInformation("Sending email to {to} with subject '{subject}'",
                    to, subject);

                await _emailService.SendAsync(to, subject, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in SendEmailAsync(): {0}",
                    ex.ToString());
            }
            
        }
    }
}
