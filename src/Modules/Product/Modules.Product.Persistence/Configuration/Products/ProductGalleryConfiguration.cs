namespace Modules.Product.Persistence.Configuration.Products;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Product.Domain.Entities.Products;

public sealed class ProductGalleryConfiguration
    : IEntityTypeConfiguration<ProductGallery>
{
    public void Configure(EntityTypeBuilder<ProductGallery> builder)
    {
        builder.ToTable("ProductGalleries");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DisplayPriority)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.ImageName)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasIndex(x => new
        {
            x.ProductId,
            x.DisplayPriority
        });

        builder.HasOne(x => x.Product)
            .WithMany(x => x.ProductGalleries)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
