using BookStore.Domain.Entities;
using BookStore.Infrastructure.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Mappings;

public class AuthorMap : EntityMap<Author>
{
    protected override void Map(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Authors");

        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.UniqueId).IsRequired().HasColumnName("UniqueId");
        builder.Property(_ => _.FirstName).IsRequired().HasColumnName("FirstName").HasMaxLength(32);
        builder.Property(_ => _.MiddleName).IsRequired().HasColumnName("MiddleName").HasMaxLength(32);
        builder.Property(_ => _.LastName).IsRequired().HasColumnName("LastName").HasMaxLength(32);

        builder.HasMany(_ => _.Books).WithOne(_ => _.Author).HasForeignKey("AuthorId").IsRequired();
    }
}
