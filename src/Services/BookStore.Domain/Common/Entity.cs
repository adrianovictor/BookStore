using BookStore.Domain.Common;

namespace BookStore.Domain.Common;

public abstract class Entity<TEntity> : IEntity<TEntity>
    where TEntity: class
{
    public int Id { get; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public abstract bool SameIdentityAs(TEntity entity);
    public abstract int GetIdentityHashCode();
    public abstract override string ToString();

    public static bool operator ==(Entity<TEntity> left, Entity<TEntity> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TEntity> left, Entity<TEntity> right)
    {
        return !Equals(left, right);
    }

    public override bool Equals(object obj)
    {
        if (IsTransient())
            return base.Equals(obj);

        return SameIdentityAs(obj as TEntity);
    }

    public virtual bool IsPersisted()
    {
        return !IsTransient();
    }

    protected virtual bool IsTransient()
    {
        return Id == 0;
    }    
}
