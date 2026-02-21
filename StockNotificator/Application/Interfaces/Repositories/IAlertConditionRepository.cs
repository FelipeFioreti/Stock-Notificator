using StockNotificator.Domain.Entities;

namespace StockNotificator.Application.Interfaces.Repositories
{
    public interface IAlertConditionRepository : IBaseRepository<AlertCondition>
    {
        Task<IEnumerable<AlertCondition>> GetByStock(Guid stockId);
    }
}
