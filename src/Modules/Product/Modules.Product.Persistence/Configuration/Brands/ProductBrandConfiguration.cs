namespace Modules.Product.Persistence.Configuration.Brands;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Product.Domain.Entities.Brands;

public sealed class ProductBrandConfiguration
    : IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        builder.ToTable("ProductBrands");

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

        builder.HasIndex(x => x.Title);

        builder.HasIndex(x => x.UrlName)
            .IsUnique();

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.ProductSelectedBrands)
            .WithOne(x => x.ProductBrand)
            .HasForeignKey(x => x.ProductBrandId);
    }
}
