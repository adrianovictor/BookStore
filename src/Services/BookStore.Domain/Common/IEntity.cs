namespace BookStore.Domain.Common;

public interface IEntity<in TEntity> : IAudit
    where TEntity: class
{
    bool SameIdentityAs(TEntity entity);
    int GetIdentityHashCode();
    bool IsPersisted();
}
