using StockNotificator.Domain.Entities;

namespace StockNotificator.Application.Interfaces.Providers
{
    public interface IQuoteProvider
    {
        Task<StockQuote?> GetQuoteAsync(string ticker);
    }
}
