namespace Modules.Product.Persistence.Configuration.Products;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Product.Domain.Entities.Products;

public sealed class ProductDiscountUseConfiguration
    : IEntityTypeConfiguration<ProductDiscountUse>
{
    public void Configure(EntityTypeBuilder<ProductDiscountUse> builder)
    {
        builder.ToTable("ProductDiscountUses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.ProductDiscountId)
            .IsRequired();

        // Product relation
        builder.HasOne(x => x.Product)
            .WithMany(x => x.ProductDiscounts)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Discount relation
        builder.HasOne(x => x.ProductDiscount)
            .WithMany()
            .HasForeignKey(x => x.ProductDiscountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.ProductId);

        builder.HasIndex(x => x.ProductDiscountId);
    }
}
