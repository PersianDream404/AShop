namespace Modules.Product.Persistence.Configuration.Categories;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Product.Domain.Entities.Categories;

public sealed class ProductCategoryConfiguration
    : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable("ProductCategories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.UrlName)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.Image)
            .HasMaxLength(250);

        builder.Property(x => x.Icon)
            .HasMaxLength(250);

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.HasIndex(x => x.UrlName)
            .IsUnique();

        builder.HasIndex(x => x.Title);

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.ProductSelectedCategories)
            .WithOne(x => x.ProductCategory)
            .HasForeignKey(x => x.ProductCategoryId);
    }
}
