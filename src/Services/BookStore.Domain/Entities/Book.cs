using BookStore.Domain.Common;
using BookStore.Domain.Enum;

namespace BookStore.Domain.Entities;

public class Book : Entity<Book>
{
    public Guid UniqueId { get; protected set; }
    public string Title { get; protected set; }
    public string Resume { get; protected set; }
    public string ISBN { get; protected set; }
    public DateTimeOffset PublishedAt { get; protected set; }
    public string Edition { get; protected set; }
    public BookStatus Status { get; protected set; }

    protected Book() 
    {
        UniqueId = Guid.NewGuid();
    }

    public Book(string title, string resume, string isbn, DateTimeOffset publishedAt, string edition, BookStatus status = BookStatus.Active) : this()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(title));
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(resume));
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(isbn));

        Title = title;
        Resume = resume;
        ISBN = isbn;
        PublishedAt = publishedAt;
        Edition = edition;
        Status = status;
    }

    public static Book Create(string title, string resume, string isbn, DateTimeOffset publishedAt, string edition, BookStatus status = BookStatus.Active)
    {
        return new Book(title, resume, isbn, publishedAt, edition, status);
    }
}
