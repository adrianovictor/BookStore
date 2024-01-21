using BookStore.Domain.Entities;

namespace BookStore.Domain.Repository;

public interface IAuthorRepository : IDisposable
{
    IQueryable<Author> GetAllAsync();
    Task<Author> SaveAsync(Author author);
    Task DeleteAsync(Author author);
}
