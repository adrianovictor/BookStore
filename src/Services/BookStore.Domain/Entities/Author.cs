using BookStore.Domain.Common;

namespace BookStore.Domain.Entities;

public class Author : Entity<Author>
{
    public Guid UniqueId { get; protected set; }
    public string FirstName { get; protected set; }
    public string MiddleName { get; protected set; }
    public string LastName { get; protected set; }
    public ICollection<Book> Books { get; } = new List<Book>();
    
    protected Author()
    {
        UniqueId = Guid.NewGuid();
    }

    public Author(string firstName, string middleName, string lastName)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
    }

    public static Author Create(string firstName, string middleName, string lastName)
    {
        return new Author(firstName, middleName, lastName);
    }

    public void ChangeName(string firstName, string middleName, string lastName)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
    }

    #region Comparison Methods
    public override bool SameIdentityAs(Author entity)
    {
        return entity is not null && UniqueId.Equals(entity.UniqueId);
    }

    public override int GetIdentityHashCode()
    {
        return UniqueId.GetHashCode();
    }

    public override string ToString()
    {
        return $"Author {FirstName} {MiddleName} {LastName}";
    }    
    #endregion    
}
