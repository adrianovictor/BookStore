using BookStore.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Mappings.Common;

public abstract class StatusableMap<TEntity> : EntityMap<TEntity>
    where TEntity : Statusable<TEntity>, IEntity<TEntity>
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(_ => _.Status).IsRequired();

        builder.Ignore(_ => _.IsActive);
        builder.Ignore(_ => _.IsInactive);
        builder.Ignore(_ => _.IsDraft);
        builder.Ignore(_ => _.IsDeleted);

        base.Configure(builder);
    }
}
