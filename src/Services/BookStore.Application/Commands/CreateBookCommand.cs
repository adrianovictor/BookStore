using BookStore.Application.Results.Command;
using MediatR;

namespace BookStore.Application.Commands;

public class CreateBookCommand : IRequest<CommandResult>
{
    public string Title { get; }
    public string Resume { get; }
    public string ISBN { get; }
    public DateTimeOffset PublishedAt { get; }
    public string Edition { get; }
    public string FirstName { get; } 
    public string MiddleName { get; } 
    public string LastName { get; }
    public string Status { get; }

    public CreateBookCommand(string title, string resume, string isbn, DateTimeOffset publishedAt, string edition, string firstName, string middleName, string lastName, string status)
    {
        Title = title;
        Resume = resume;
        ISBN = isbn;
        PublishedAt = publishedAt;
        Edition = edition;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Status = status;
    }
}

public class AuthorCommand
{
    public string FirstName { get; }
    public string MiddleName { get; }
    public string LastName { get; }
}
