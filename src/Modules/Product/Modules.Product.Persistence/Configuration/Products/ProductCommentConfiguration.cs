namespace Modules.Product.Persistence.Configuration.Products;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Product.Domain.Entities.Products;

public sealed class ProductCommentConfiguration
    : IEntityTypeConfiguration<ProductComment>
{
    public void Configure(EntityTypeBuilder<ProductComment> builder)
    {
        builder.ToTable("ProductComments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.Message)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Email)
            .HasMaxLength(300);

        builder.Property(x => x.StrongPoint)
            .HasMaxLength(200);

        builder.Property(x => x.WeakPoint)
            .HasMaxLength(200);

        builder.HasOne(x => x.Product)
            .WithMany(x => x.ProductComments)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.ProductId);
    }
}
