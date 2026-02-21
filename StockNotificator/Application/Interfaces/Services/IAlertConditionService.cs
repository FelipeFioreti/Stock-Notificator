using StockNotificator.Domain.Entities;

namespace StockNotificator.Application.Interfaces.Services
{
    public interface IAlertConditionService : IBaseService<AlertCondition>
    {
        Task<IEnumerable<AlertCondition>> GetByStock(Guid stockId);
    }
}
