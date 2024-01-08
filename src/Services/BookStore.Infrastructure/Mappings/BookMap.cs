using BookStore.Domain.Entities;
using BookStore.Infrastructure.Mappings.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Mappings;

public class BookMap : StatusableMap<Book>
{
    protected override void Map(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.UniqueId).IsRequired().HasColumnName("UniqueId");
        builder.Property(_ => _.Title).IsRequired().HasColumnName("Title").HasMaxLength(100);
        builder.Property(_ => _.ISBN).IsRequired().HasColumnName("ISBN").HasMaxLength(32);
        builder.Property(_ => _.Resume).IsRequired().HasColumnName("Resumo").HasMaxLength(255);
        builder.Property(_ => _.PublishedAt).HasColumnName("PublishedAt");
        builder.Property(_ => _.Edition).IsRequired().HasColumnName("Edition").HasMaxLength(32);
    }
}
