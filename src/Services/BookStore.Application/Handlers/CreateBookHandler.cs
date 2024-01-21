using BookStore.Application.Commands;
using BookStore.Application.Results.Command;
using BookStore.Common.Extensions;
using BookStore.Domain.Entities;
using BookStore.Domain.Enum;
using BookStore.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Handlers;

public class CreateBookHandler(IBookRepository bookRepository, IAuthorRepository authorRepository) : IRequestHandler<CreateBookCommand, CommandResult>
{
    private readonly IBookRepository _repository = bookRepository;
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<CommandResult> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetAllAsync().FirstOrDefaultAsync(_ => _.FirstName == request.FirstName && _.MiddleName == request.MiddleName && _.LastName == request.LastName);
        if (author is null) 
        {
            author = Author.Create(request.FirstName, request.MiddleName, request.LastName);
        }

        var book = await _repository.GetAllAsync().FirstOrDefaultAsync(_ => _.Title.Equals(request.Title));
        if (book is not null) 
        {
            return CommandResult.Conflict("Título já cadastrado");
        }

        var entity = Book.Create(request.Title, request.Resume, request.ISBN, request.PublishedAt, request.Edition, author, request.Status.ToEnum<Status>());
        book = await _repository.SaveAsync(book);

        return CommandResult.Created(new { Id = book.Id }.ToDynamic());        
    }
}
