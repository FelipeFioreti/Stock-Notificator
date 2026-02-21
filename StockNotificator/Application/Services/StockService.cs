using StockNotificator.Application.Interfaces.Repositories;
using StockNotificator.Application.Interfaces.Services;
using StockNotificator.Domain.Entities;

namespace StockNotificator.Application.Services
{

    public class StockService(IUnitOfWork unitOfWork, IStockRepository repository) : BaseService<Stock, IStockRepository>(repository, unitOfWork), IStockService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<IEnumerable<Stock>> GetByUsers()
        {
             return await _unitOfWork.Stocks.GetByUsers();
        }
    }
}
