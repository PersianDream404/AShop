namespace Modules.Product.Persistence.Configuration.FeaturesCategories;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Product.Domain.Entities.FeaturesCategories;

public sealed class ProductFeaturesCategoryConfiguration
    : IEntityTypeConfiguration<FeaturesCategory>
{
    public void Configure(EntityTypeBuilder<FeaturesCategory> builder)
    {
        builder.ToTable("ProductFeaturesCategories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FeatureCategoryTitle)
            .IsRequired()
            .HasMaxLength(300);

        builder.HasIndex(x => x.FeatureCategoryTitle);

        builder.HasMany(x => x.ProductFeatures)
            .WithOne(x => x.ProductFeaturesCategory)
            .HasForeignKey(x => x.ProductFeaturesCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
