using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Persistence.Configuration.Products;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductConfiguration : IEntityTypeConfiguration<Modules.Product.Domain.Entities.Products.Product>
{
    public void Configure(EntityTypeBuilder<Modules.Product.Domain.Entities.Products.Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Code)
            .HasMaxLength(300);

        builder.Property(x => x.Price)
            .IsRequired();

        builder.Property(x => x.ShortDescription)
            .HasMaxLength(300);

        builder.Property(x => x.Description);

        builder.Property(x => x.Image)
            .HasMaxLength(500);



        builder.Property(x => x.ViewCount)
            .HasDefaultValue(0);

    

        builder.HasMany(x => x.ProductSelectedCategories)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.ProductSelectedColors)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.ProductGalleries)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.ProductFeatures)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.ProductDiscounts)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);
    }
}
