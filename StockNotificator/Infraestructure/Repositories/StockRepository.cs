using Microsoft.EntityFrameworkCore;
using StockNotificator.Application.Interfaces.Repositories;
using StockNotificator.Domain.Entities;
using StockNotificator.Infraestructure.Context;

namespace StockNotificator.Infraestructure.Repositories
{
    public class StockRepository(ApplicationDbContext context) : BaseRepository<Stock>(context), IStockRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<IEnumerable<Stock>> GetByUsers()
        {
            return await _context.Stocks
            .AsNoTracking()
            .Where(s =>
                s.UserStocks.Any())
            .ToListAsync();
        }
    }
}
