using BookStore.Domain.Entities;
using BookStore.Domain.Repository;
using BookStore.Infrastructure.Repositories.Common;

namespace BookStore.Infrastructure.Repositories;

public class AuthorRepository(IRepository repository) : IAuthorRepository, IDisposable
{
    private readonly IRepository _repository = repository;
    private bool _disposed = false;

    public Task DeleteAsync(Author author)
    {
        return _repository.DeleteAsync(author);
    }

    public IQueryable<Author> GetAllAsync()
    {
        return _repository.GetAllAsync<Author>();
    }

    public Task<Author> SaveAsync(Author author)
    {
        return _repository.SaveAsync(author);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            _disposed = true;
        }
    }
}
