using BookStore.Domain.Entities;

namespace BookStore.Domain.Repository;

public interface IBookRepository : IDisposable
{
    IQueryable<Book> GetAllAsync();
    Task<Book> UpdateAsync(Book book);
    Task DeleteAsync(Book book);
}
