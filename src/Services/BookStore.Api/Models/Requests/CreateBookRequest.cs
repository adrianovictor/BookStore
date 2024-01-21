using System.Text.Json.Serialization;

namespace BookStore.Api.Models.Requests;

public class CreateBookRequest
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    public string Resume { get; set; }
    public string ISBN { get; set; }
    public DateTimeOffset PublishedAt { get; set; }
    public string Edition { get; set; }
    public string FirstName { get; set; } 
    public string MiddleName { get; set; } 
    public string LastName { get; set; }
    public string Status { get; set; }
}
