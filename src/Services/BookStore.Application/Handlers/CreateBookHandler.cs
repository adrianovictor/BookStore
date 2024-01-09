using BookStore.Application.Commands;
using BookStore.Domain.Repository;
using MediatR;

namespace BookStore.Application.Handlers;

public class CreateBookHandler(IBookRepository bookRepository)
{
    private readonly IBookRepository _repository = bookRepository;
}
