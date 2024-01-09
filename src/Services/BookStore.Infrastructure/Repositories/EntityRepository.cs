using BookStore.Domain.Common;
using BookStore.Infrastructure.DataContexts;
using BookStore.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure;

public class EntityRepository(BookStoreContext dataContext) : IRepository, IDisposable
{
    private readonly BookStoreContext _dataContext = dataContext;
    private bool _disposed = false;

    public Task DeleteAsync<TEntity>(TEntity entity) where TEntity: class, IEntity<TEntity>
    {
        _dataContext.Set<TEntity>().Remove(entity);
        _dataContext.SaveChanges();

        return Task.CompletedTask;
    }

    public IQueryable<TEntity> GetAllAsync<TEntity>() where TEntity: class, IEntity<TEntity>
    {
        return _dataContext.Set<TEntity>();
    }

    public async Task<TEntity> SaveAsync<TEntity>(TEntity entity, bool disableValidationOnSave) where TEntity: class, IEntity<TEntity>
    {
        if (entity.IsPersisted())
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
        }
        else
        {
            _dataContext.Set<TEntity>().Add(entity);
        }

        await _dataContext.SaveChangesAsync();
        return await Task.FromResult(entity);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
            }

            _disposed = true;
        }
    }
}
