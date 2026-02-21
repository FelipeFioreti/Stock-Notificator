using Microsoft.EntityFrameworkCore;
using StockNotificator.Application.Interfaces.Repositories;
using StockNotificator.Domain.Entities;
using StockNotificator.Infraestructure.Context;

namespace StockNotificator.Infraestructure.Repositories
{
    public class AlertConditionRepository(ApplicationDbContext context) : BaseRepository<AlertCondition>(context), IAlertConditionRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<IEnumerable<AlertCondition>> GetByStock(Guid stockId)
        {
            return await _context.AlertConditions
            .AsNoTracking()
            .Include(a => a.UserStock)
                .ThenInclude(us => us.User)
            .Where(a =>
                a.UserStock.StockId == stockId)
            .ToListAsync();
        }
    }
}
