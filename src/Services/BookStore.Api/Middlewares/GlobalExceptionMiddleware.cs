
using BookStore.Application.Results.Web.Resources;
using Newtonsoft.Json;

namespace BookStore.Api.Middlewares;

public class GlobalExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
		try
		{
			await next(context);
		}
		catch (Exception exception)
		{
			// TODO: adicionar log aqui
			await HandleExceptionAsync(context, exception);
		}
    }

	private async Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		OperationResult operationResult = new OperationResult();

		if (exception is ArgumentException argumentException)
		{
			var message = argumentException.Message;

			operationResult = new OperationResult
			{
				Errors = new[]
				{
					new OperationResult.OperationErrorMessage
					{
						ErrorCode = "Parametro inválido.",
						Message = string.Format(message, argumentException.ParamName)
					}
				},
				StatusCode = StatusCodes.Status400BadRequest
			};
		}

		var json = JsonConvert.SerializeObject(operationResult);

		context.Response.ContentType = "application/json";
		context.Response.StatusCode = StatusCodes.Status400BadRequest;
		await context.Response.WriteAsync(json);
	}
}
