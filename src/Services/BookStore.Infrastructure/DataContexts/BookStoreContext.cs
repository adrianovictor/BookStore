using BookStore.Infrastructure.DataContexts.Common;
using BookStore.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.DataContexts;

public class BookStoreContext(string connectionString) : BaseContext<BookStoreContext>(connectionString)
{
    protected override void OnCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookMap());
        modelBuilder.ApplyConfiguration(new AuthorMap());
    }
}
