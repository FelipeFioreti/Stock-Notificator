using StockNotificator.Domain.Entities;

namespace StockNotificator.Application.Interfaces.Repositories
{
    public interface IStockRepository : IBaseRepository<Stock>
    {
        Task<IEnumerable<Stock>> GetByUsers();
    }
}
