namespace Modules.Product.Persistence.Configuration.Products;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Product.Domain.Entities.Products;

public sealed class ProductFeaturesConfiguration
    : IEntityTypeConfiguration<ProductSelectedFeatures>
{
    public void Configure(EntityTypeBuilder<ProductSelectedFeatures> builder)
    {
        builder.ToTable("ProductFeatures");

        builder.HasKey(x => x.Id);

        //builder.Property(x => x.FeatureTitle)
        //    .IsRequired()
        //    .HasMaxLength(300);

        builder.Property(x => x.FeatureValue)
            .HasMaxLength(300)
            .IsRequired();

        // Product relation
        builder.HasOne(x => x.Product)
            .WithMany(x => x.ProductFeatures)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Category relation (optional)
        builder.HasOne(x => x.ProductFeaturesCategory)
            .WithMany(x => x.ProductFeatures)
            .HasForeignKey(x => x.ProductFeaturesCategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.ProductId);

        builder.HasIndex(x => x.ProductFeaturesCategoryId);
    }
}
