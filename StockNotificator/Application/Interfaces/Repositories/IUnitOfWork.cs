namespace StockNotificator.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IStockRepository Stocks { get; }
        IUserStockRepository UserStocks { get; }
        IStockQuoteRepository StockQuotes { get; }
        IAlertConditionRepository AlertConditions { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
