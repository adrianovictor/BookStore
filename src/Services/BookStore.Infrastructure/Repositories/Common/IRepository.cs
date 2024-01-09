using BookStore.Domain.Common;

namespace BookStore.Infrastructure.Repositories.Common;

public interface IRepository : IDisposable
{
    IQueryable<TEntity> GetAllAsync<TEntity>() where TEntity : class, IEntity<TEntity>;
    
    Task<TEntity> SaveAsync<TEntity>(TEntity entity, bool disableValidationOnSave = false) where TEntity : class, IEntity<TEntity>;

    Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<TEntity>;
}
