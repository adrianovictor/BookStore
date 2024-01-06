
namespace BookStore.Api.Middlewares;

public class GlobalExceptionMiddleware : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        throw new NotImplementedException();
    }
}
