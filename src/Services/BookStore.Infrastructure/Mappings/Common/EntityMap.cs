using BookStore.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Mappings.Common;

public abstract class EntityMap<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity<TEntity>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(_ => _.CreatedAt).IsRequired();
        builder.Property(_ => _.UpdatedAt);

        Map(builder);
    }

    protected abstract void Map(EntityTypeBuilder<TEntity> builder);
}
