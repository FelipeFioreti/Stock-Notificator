using Microsoft.EntityFrameworkCore.Storage;
using StockNotificator.Application.Interfaces.Repositories;
using StockNotificator.Infraestructure.Context;

namespace StockNotificator.Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Stocks = new StockRepository(_context);
            UserStocks = new UserStockRepository(_context);
            StockQuotes = new StockQuoteRepository(_context);
            AlertConditions = new AlertConditionRepository(_context);
        }
        public IUserRepository Users { get; }
        public IStockRepository Stocks { get; }
        public IUserStockRepository UserStocks { get; }
        public IStockQuoteRepository StockQuotes { get; }
        public IAlertConditionRepository AlertConditions { get; }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }
        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction has not been started.");
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                await _transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync(cancellationToken);
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
