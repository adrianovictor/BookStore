using System.Dynamic;
using BookStore.Application.Results.Command;
using BookStore.Application.Results.Web.Resources;
using BookStore.Common.Extensions;

namespace BookStore.Api.Common.Extensions;

public static class CommandResultExtensions
{
    public static IResult ToResult(this ICommandResult commandResult)
    {
        var resultHandler = new Dictionary<int, Func<IResult>>()
        {
            { StatusCodes.Status201Created, CreatedResult(commandResult) },
            { StatusCodes.Status200OK, OkResult(commandResult) },
            { StatusCodes.Status400BadRequest, BadRequestResult(commandResult) },
            { StatusCodes.Status409Conflict, ConflictResult(commandResult) }
    };

        return resultHandler[commandResult.HttpCode].Invoke();
    }

    private static Func<IResult> CreatedResult(ICommandResult commandResult)
    {
        return () =>
        {
            if (commandResult.DataResult is not null && ObjectExtensions.HasProperty(commandResult.DataResult, "Id"))
            {
                var routerParameters = new { commandResult?.DataResult?.Id };

                IDictionary<string, object> data = null;
                IDictionary<string, object> tempDataResult = commandResult.DataResult;

                if (tempDataResult.Count() > 1)
                {
                    data = new ExpandoObject();
                    foreach (var kvp in tempDataResult)
                    {
                        if (!kvp.Key.Equals("Id"))
                            data.Add(kvp);
                    }
                }

                return Results.Created(string.Empty, new { resourceId = routerParameters, data });
            }

            return Results.Created(string.Empty, commandResult.DataResult);
        };
    }

    private static Func<IResult> OkResult(ICommandResult commandResult)
    {
        return () =>
        {
            return Results.Ok(commandResult.DataResult);
        };
    }

    private static Func<IResult> BadRequestResult(ICommandResult commandResult)
    {
        return () =>
        {
            return Results.BadRequest<OperationResult>(GetErrors(commandResult));
        };
    }

    private static Func<IResult> ConflictResult(ICommandResult commandResult)
    {
        return () =>
        {
            return Results.Conflict<OperationResult>(GetErrors(commandResult));
        };
    }

    private static OperationResult GetErrors(ICommandResult commandResult)
    {
        return new OperationResult
        {
            Errors = commandResult.ErrorCodes.Select((err, i) =>
            {
                var messesage = err;

                return new OperationResult.OperationErrorMessage
                {
                    ErrorCode = err,
                    Message = messesage
                };
            }),
            StatusCode = commandResult.HttpCode
        };
    }
}
