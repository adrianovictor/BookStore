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
        throw new NotImplementedException();
    }

    public IQueryable<Author> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Author> UpdateAsync(Author author)
    {
        throw new NotImplementedException();
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
