namespace StockNotificator.Application.Interfaces.Repositories
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string body);
    }
}
