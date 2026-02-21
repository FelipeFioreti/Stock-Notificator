using StockNotificator.Application.Interfaces.Repositories;
using StockNotificator.Application.Interfaces.Services;
using StockNotificator.Domain.Entities;
using StockNotificator.Domain.Enums;

namespace StockNotificator.Application.Services
{
    public class AlertConditionService(IUnitOfWork unitOfWork, IAlertConditionRepository repository) : BaseService<AlertCondition, IAlertConditionRepository>(repository, unitOfWork), IAlertConditionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<IEnumerable<AlertCondition>> GetByStock(Guid stockId)
        {
            return await _unitOfWork.AlertConditions.GetByStock(stockId);
        }
    }
}
