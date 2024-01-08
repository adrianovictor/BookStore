using BookStore.Infrastructure.DataContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(provider => new BookStoreContext(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
