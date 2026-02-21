using StockNotificator.Application.Interfaces.Repositories;
using StockNotificator.Application.Interfaces.Services;
using StockNotificator.Domain.Entities;

namespace StockNotificator.Application.Services
{
    public class UserStockService(IUnitOfWork unitOfWork, IUserStockRepository repository) : BaseService<UserStock, IUserStockRepository>(repository, unitOfWork), IUserStockService
    {
    }
}
