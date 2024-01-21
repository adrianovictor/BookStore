namespace BookStore.Application.Results.Command;

public interface ICommandResult
{
    IEnumerable<string> ErrorCodes { get; }
    IEnumerable<string[]> ErrorCodeParameters { get; }
    int HttpCode { get; }
    dynamic DataResult { get; }
    bool HasError { get; }
    bool HasParameters { get; }
}
