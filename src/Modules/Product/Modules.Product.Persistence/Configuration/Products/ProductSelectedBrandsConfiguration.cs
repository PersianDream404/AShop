namespace Modules.Product.Persistence.Configuration.Products;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Product.Domain.Entities.Products;

public sealed class ProductSelectedBrandsConfiguration
    : IEntityTypeConfiguration<ProductSelectedBrands>
{
    public void Configure(EntityTypeBuilder<ProductSelectedBrands> builder)
    {
        builder.ToTable("ProductSelectedBrands");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.ProductBrandId)
            .IsRequired();

        // Product relation
        builder.HasOne(x => x.Product)
            .WithMany(x => x.ProductSelectedBrands)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Brand relation
        builder.HasOne(x => x.ProductBrand)
            .WithMany(x => x.ProductSelectedBrands)
            .HasForeignKey(x => x.ProductBrandId)
            .OnDelete(DeleteBehavior.Cascade);

        // جلوگیری از تکرار برند برای یک محصول
        builder.HasIndex(x => new { x.ProductId, x.ProductBrandId })
            .IsUnique();
    }
}

public sealed class ProductSelectedColorsConfiguration
    : IEntityTypeConfiguration<ProductSelectedColors>
{
    public void Configure(EntityTypeBuilder<ProductSelectedColors> builder)
    {
        builder.ToTable("ProductSelectedColors");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.ProductColorId)
            .IsRequired();

        // Product relation
        builder.HasOne(x => x.Product)
            .WithMany(x => x.ProductSelectedColors)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Color relation
        builder.HasOne(x => x.ProductColor)
            .WithMany(x => x.ProductSelectedColors)
            .HasForeignKey(x => x.ProductColorId)
            .OnDelete(DeleteBehavior.Cascade);

        // جلوگیری از تکرار برند برای یک محصول
        builder.HasIndex(x => new { x.ProductId, x.ProductColorId })
            .IsUnique();
    }
}

public sealed class ProductSelectedFeaturesConfiguration
    : IEntityTypeConfiguration<ProductSelectedFeatures>
{
    public void Configure(EntityTypeBuilder<ProductSelectedFeatures> builder)
    {
        builder.ToTable("ProductSelectedFeatures");

        builder.HasKey(x => x.Id);

        //builder.Property(x => x.ProductId)
        //    .IsRequired();

        //builder.Property(x => x.ProductColorId)
        //    .IsRequired();

        // Product relation
        builder.HasOne(x => x.Product)
            .WithMany(x => x.ProductFeatures)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Color relation
        builder.HasOne(x => x.ProductFeaturesCategory)
            .WithMany(x => x.ProductFeatures)
            .HasForeignKey(x => x.ProductFeaturesCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ProductFeatures)
            .WithMany(x => x.ProductSelectedFeatures)
            .HasForeignKey(x => x.ProductFeaturesId)
            .OnDelete(DeleteBehavior.Restrict);

        // جلوگیری از تکرار برند برای یک محصول
        //builder.HasIndex(x => new { x.ProductId, x.ProductColorId })
        //    .IsUnique();
    }
}