using StockNotificator.Application.Interfaces.Repositories;
using StockNotificator.Application.Interfaces.Services;
using System.Linq.Expressions;

namespace StockNotificator.Application.Services
{
    public class BaseService<TEntity, TRepository>(TRepository repository, IUnitOfWork unitOfWork) : IBaseService<TEntity> where TEntity : class where TRepository : IBaseRepository<TEntity>
    {
        private readonly TRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            var createdEntity = await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return createdEntity;
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddRangeAsync(entities, cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        public async Task Update(TEntity entity)
        {
            await _unitOfWork.BeginTransactionAsync();
            _repository.Update(entity);
            await _unitOfWork.CommitTransactionAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _repository.FindAsync(predicate, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }
        public async Task Remove(TEntity entity)
        {
            await _unitOfWork.BeginTransactionAsync();
            _repository.Remove(entity);
            await _unitOfWork.CommitTransactionAsync();
        }
        public async Task RemoveRange(IEnumerable<TEntity> entities)
        {
            await _unitOfWork.BeginTransactionAsync();
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitTransactionAsync();
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            return await _repository.CountAsync(predicate, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _repository.ExistsAsync(id, cancellationToken);  
        }
    }
}
