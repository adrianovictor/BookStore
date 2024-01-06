using BookStore.Domain.Common;

namespace BookStore.Domain.Common;

public abstract class Entity<TEntity> : IEntity<TEntity>
    where TEntity: class
{
    public int Id { get; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
