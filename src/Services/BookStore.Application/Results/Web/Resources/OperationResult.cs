using System.Text.Json.Serialization;

namespace BookStore.Application.Results.Web.Resources;

public class OperationResult : IOperationResult
{
    public int StatusCode { get; set; }

    [JsonPropertyName("resourceId")]
    public int ResourceId { get; set; }

    [JsonPropertyName("data")]
    public dynamic Data { get; set; }

    [JsonPropertyName("messages")]
    public IEnumerable<OperationErrorMessage> Errors { get; set; }

    //[JsonPropertyName("fields")]
    //public IEnumerable<OperationErrorField> Fields { get; set; }

    public class OperationErrorMessage
    {
        [JsonPropertyName("errorCode")]
        public string ErrorCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
