using System.Reflection;
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

var app = builder.Build();

app.MapGet("/", async (IMediator mediator) => 
{ 
    return Results.Ok(new { result = "Olá mundo." });
});

app.Run();
