namespace BookStore.Domain.Common;

public interface IAudit
{
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset? UpdatedAt { get; set; }
}
