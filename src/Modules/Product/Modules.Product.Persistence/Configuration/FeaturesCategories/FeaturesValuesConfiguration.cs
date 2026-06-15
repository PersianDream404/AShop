namespace Modules.Product.Persistence.Configuration.FeaturesCategories;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Product.Domain.Entities.Features;

public sealed class FeaturesValuesConfiguration
    : IEntityTypeConfiguration<FeaturesValues>
{
    public void Configure(EntityTypeBuilder<FeaturesValues> builder)
    {
        builder.ToTable("FeaturesValues");

        builder.HasKey(x => x.Id);

        //builder.Property(x => x.FeatureTitle)
        //    .IsRequired()
        //    .HasMaxLength(300);

        builder.Property(x => x.FeatureValue)
            .HasMaxLength(300)
            .IsRequired();



        // Category relation (optional)
        builder.HasOne(x => x.ProductFeaturesCategory)
            .WithMany(x => x.FeaturesValues)
            .HasForeignKey(x => x.ProductFeaturesCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ProductFeatures)
            .WithMany(x => x.FeaturesValues)
            .HasForeignKey(x => x.ProductFeaturesId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ProductFeaturesCategoryId);
    }
}
