using BookStore.Domain.Entities;
using BookStore.Domain.Repository;
using BookStore.Infrastructure.Repositories.Common;

namespace BookStore.Infrastructure.Repositories;

public class BookRepository(IRepository repository) : IBookRepository, IDisposable
{
    private readonly IRepository _repository = repository;
    private bool _disposed = false;

    public Task DeleteAsync(Book book)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Book> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Book> UpdateAsync(Book book)
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
