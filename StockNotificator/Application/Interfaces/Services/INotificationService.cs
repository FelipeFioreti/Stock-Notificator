namespace StockNotificator.Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
