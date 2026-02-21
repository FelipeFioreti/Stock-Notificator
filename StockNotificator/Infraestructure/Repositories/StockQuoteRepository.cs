using StockNotificator.Application.Interfaces.Repositories;
using StockNotificator.Domain.Entities;
using StockNotificator.Infraestructure.Context;

namespace StockNotificator.Infraestructure.Repositories
{
    public class StockQuoteRepository(ApplicationDbContext context) : BaseRepository<StockQuote>(context), IStockQuoteRepository
    {
    }
}
