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
        return _repository.DeleteAsync(book);
    }

    public IQueryable<Book> GetAllAsync()
    {
        return _repository.GetAllAsync<Book>();
    }
    
    public Task<Book> SaveAsync(Book book)
    {
        return _repository.SaveAsync(book);
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
