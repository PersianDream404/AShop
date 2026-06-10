namespace Modules.Product.Persistence.Configuration.Products;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Product.Domain.Entities.Products;

public sealed class ProductSelectedCategoryConfiguration
    : IEntityTypeConfiguration<ProductSelectedCategory>
{
    public void Configure(EntityTypeBuilder<ProductSelectedCategory> builder)
    {
        builder.ToTable("ProductSelectedCategories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.ProductCategoryId)
            .IsRequired();

        // Product relation
        builder.HasOne(x => x.Product)
            .WithMany(x => x.ProductSelectedCategories)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Category relation
        builder.HasOne(x => x.ProductCategory)
            .WithMany(x => x.ProductSelectedCategories)
            .HasForeignKey(x => x.ProductCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        // جلوگیری از ثبت تکراری
        builder.HasIndex(x => new { x.ProductId, x.ProductCategoryId })
            .IsUnique();
    }
}
