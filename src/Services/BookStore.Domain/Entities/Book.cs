using BookStore.Domain.Common;
using BookStore.Domain.Enum;

namespace BookStore.Domain.Entities;

public class Book : Statusable<Book>
{
    #region Properties
    public Guid UniqueId { get; protected set; }
    public string Title { get; protected set; }
    public string Resume { get; protected set; }
    public string ISBN { get; protected set; }
    public DateTimeOffset PublishedAt { get; protected set; }
    public string Edition { get; protected set; }     
    public Author Author { get; protected set; } 
    #endregion

    #region Constructors
    protected Book() 
    {
        UniqueId = Guid.NewGuid();
    }

    public Book(string title, string resume, string isbn, DateTimeOffset publishedAt, string edition, Author author, Status status = Status.Active) : this()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(title));
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(resume));
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(isbn));
        ArgumentNullException.ThrowIfNull(nameof(author));

        Title = title;
        Resume = resume;
        ISBN = isbn;
        PublishedAt = publishedAt;
        Edition = edition;
        Author = author;
        Status = status;
    }
    #endregion

    #region Factories
    public static Book Create(string title, string resume, string isbn, DateTimeOffset publishedAt, string edition, Author author, Status status = Status.Active)
    {
        return new Book(title, resume, isbn, publishedAt, edition, author, status);
    }
    #endregion

    #region Public Methods
    public void ChangeTitle(string title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(title));

        Title = title;
    }

    public void ChangeResumo(string resume)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(resume));

        Resume = resume;
    }

    public void ChangePublishedAt(DateTimeOffset publishedAt)
    {
        PublishedAt = publishedAt;
    }
    #endregion

    #region Comparison Methods
    public override bool SameIdentityAs(Book entity)
    {
        return entity is not null && UniqueId.Equals(entity.UniqueId);
    }

    public override int GetIdentityHashCode()
    {
        return UniqueId.GetHashCode();
    }

    public override string ToString()
    {
        return $"Book {Title}";
    }    
    #endregion
}
