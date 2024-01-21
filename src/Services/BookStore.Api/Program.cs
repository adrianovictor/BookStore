using System.Reflection;
using BookStore.Api.Common.Extensions;
using BookStore.Api.Middlewares;
using BookStore.Api.Models.Requests;
using BookStore.Application.Commands;
using BookStore.Application.Handlers;
using BookStore.Domain.Repository;
using BookStore.Infrastructure;
using BookStore.Infrastructure.DataContexts;
using BookStore.Infrastructure.Repositories;
using BookStore.Infrastructure.Repositories.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(provider => new BookStoreContext(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(_ => _.RegisterServicesFromAssemblies(typeof(CreateBookHandler).GetTypeInfo().Assembly));

builder.Services.AddScoped<IRepository, EntityRepository>();
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();

builder.Services.AddTransient<GlobalExceptionMiddleware>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", async (IMediator mediator, [AsParameters] CreateBookRequest request) => 
{ 
    //return Results.Ok(new { result = "Olá mundo." });
    return (await mediator.Send(
       new CreateBookCommand(request.Title, request.Resume, request.ISBN, request.PublishedAt, request.Edition, request.FirstName, request.MiddleName, request.LastName, request.Status))
    ).ToResult();
})
.Produces(StatusCodes.Status201Created)
.Produces(StatusCodes.Status409Conflict)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status500InternalServerError);

app.Run();
