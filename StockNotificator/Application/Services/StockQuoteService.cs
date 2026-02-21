using StockNotificator.Application.Interfaces.Repositories;
using StockNotificator.Application.Interfaces.Services;
using StockNotificator.Domain.Entities;

namespace StockNotificator.Application.Services
{
    public class StockQuoteService(IUnitOfWork unitOfWork, IStockQuoteRepository repository) : BaseService<StockQuote, IStockQuoteRepository>(repository, unitOfWork), IStockQuoteService
    {
    }
}
