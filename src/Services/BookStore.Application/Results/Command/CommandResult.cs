using BookStore.Common.Extensions;

namespace BookStore.Application.Results.Command;

public class CommandResult : ICommandResult
{
    public IEnumerable<string> ErrorCodes { get; protected set; }
    public IEnumerable<string[]> ErrorCodeParameters { get; protected set; }
    public int HttpCode { get; protected set; }
    public dynamic DataResult { get; protected set; }
    public bool HasError => ErrorCodes.Any();
    public bool HasParameters => ErrorCodeParameters.Any();

    private CommandResult(IEnumerable<string> errorCodes = null, int? httpErrorCode = null)
    {
        HttpCode = httpErrorCode.GetValueOrDefault(0);
        ErrorCodes = errorCodes ?? new List<string>();
        ErrorCodeParameters = new List<string[]>();
    }

    private CommandResult(IEnumerable<string> errorCodes = null, IEnumerable<string[]> values = null, int? httpErrorCode = null)
    {
        HttpCode = httpErrorCode.GetValueOrDefault(0);
        ErrorCodes = errorCodes ?? new List<string>();
        ErrorCodeParameters = values ?? new List<string[]>();
    }

    public static CommandResult WithError(string errorCode)
    {
        return new CommandResult(new[] { errorCode }, StatusCodes.Status400BadRequest);
    }

    public static CommandResult WithError(string errorCode, string values, int? httpErrorCode = null)
    {
        return new CommandResult(new[] { errorCode }, new[] { new[] { values } }, httpErrorCode ?? StatusCodes.Status400BadRequest);
    }

    public static CommandResult WithErrors(IEnumerable<string> errorCodes, int? httpErrorCode = null)
    {
        return new CommandResult(errorCodes, httpErrorCode ?? StatusCodes.Status400BadRequest);
    }

    public static CommandResult WithErrors(IEnumerable<string> errorCodes, IEnumerable<string[]> values)
    {
        return new CommandResult(errorCodes, values, StatusCodes.Status400BadRequest);
    }

    public static CommandResult WithError(string errorCode, int httpErrorCode)
    {
        return new CommandResult(new[] { errorCode }, httpErrorCode);
    }

    public static CommandResult WithError(string errorCode, string value, int httpErrorCode)
    {
        return new CommandResult(new[] { errorCode }, new[] { new[] { value } }, httpErrorCode);
    }

    public static CommandResult WithErrors(IEnumerable<string> errorCodes, int httpErrorCode)
    {
        return new CommandResult(errorCodes, httpErrorCode);
    }

    public static CommandResult WithErrors(IEnumerable<string> errorCodes, IEnumerable<string[]> values, int httpErrorCode)
    {
        return new CommandResult(errorCodes, values, httpErrorCode);
    }

    public static CommandResult BadRequest(string errorCode)
    {
        return new CommandResult(new[] { errorCode }, StatusCodes.Status400BadRequest);
    }

    public static CommandResult NotFound(string errorCode)
    {
        return new CommandResult(new[] { errorCode }, StatusCodes.Status404NotFound);
    }

    public static CommandResult Conflict(string errorCode)
    {
        return new CommandResult(new[] { errorCode }, StatusCodes.Status409Conflict);
    }

    public static CommandResult InternalServerError(string errorCode = null)
    {
        return new CommandResult(new[] { errorCode ?? "Houve um erro interno inesperado" }, StatusCodes.Status500InternalServerError);
    }

    public static CommandResult Forbidden(string errorCode)
    {
        return new CommandResult(new[] { errorCode }, StatusCodes.Status403Forbidden);
    }

    public static CommandResult Unauthorized(string errorCode)
    {
        return new CommandResult(new[] { errorCode }, StatusCodes.Status401Unauthorized);
    }

    public static CommandResult ServiceUnavailable(string errorCode)
    {
        return new CommandResult(new[] { errorCode }, StatusCodes.Status503ServiceUnavailable);
    }

    public static CommandResult NoContent()
    {
        return new CommandResult(Enumerable.Empty<string>(), StatusCodes.Status204NoContent);
    }

    public static CommandResult Created(dynamic dataResult = null)
    {
        return new CommandResult(Enumerable.Empty<string>(), StatusCodes.Status201Created) { DataResult = dataResult };
    }

    public static CommandResult Accepted(dynamic dataResult = null)
    {
        return new CommandResult(Enumerable.Empty<string>(), StatusCodes.Status202Accepted) { DataResult = dataResult };
    }

    public static CommandResult Ok(dynamic dataResult = null)
    {
        return new CommandResult(Enumerable.Empty<string>(), StatusCodes.Status200OK) { DataResult = dataResult };
    }

    public static CommandResult MultiStatus(dynamic dataResult = null)
    {
        return new CommandResult(Enumerable.Empty<string>(), StatusCodes.Status207MultiStatus) { DataResult = dataResult };
    }

    public static CommandResult operator +(CommandResult left, CommandResult right)
    {
        if (left.HasError || right.HasError)
            return WithErrors(left.ErrorCodes.Concat(right.ErrorCodes), Math.Max(left.HttpCode, right.HttpCode));

        return Ok(((object)left.DataResult).Merge((object)right.DataResult));
    }
}
