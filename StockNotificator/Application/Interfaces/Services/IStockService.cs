using StockNotificator.Domain.Entities;

namespace StockNotificator.Application.Interfaces.Services
{
    public interface IStockService : IBaseService<Stock>
    {
        Task<IEnumerable<Stock>> GetByUsers();
    }
}
