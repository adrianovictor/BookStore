using BookStore.Domain.Enum;
using BookStore.Domain.Exceptions;

namespace BookStore.Domain.Common;

public abstract class Statusable<TEntity> : Entity<TEntity>
    where TEntity : class
{
    public Status Status { get; protected set; }

    public bool IsActive => Status == Status.Active;
    public bool IsDraft => Status == Status.Draft;
    public bool IsDeleted => Status == Status.Deleted;
    public bool IsInactive => Status == Status.Inactive;

    public virtual void ChangeStatus(Status status)
    {
        if (IsDeleted)
        {
            throw new CannotChangeStatusOfADeletedEntityException();
        }

        Status = status;
    }

    public virtual void Inactivate()
    {
        ChangeStatus(Status.Inactive);
    }

    public virtual void Delete()
    {
        ChangeStatus(Status.Deleted);
    }

    public virtual void Activate()
    {
        ChangeStatus(Status.Active);
    }
}
