namespace Modules.Product.Persistence.Configuration.Discounts;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Product.Domain.Entities.Discounts;

public sealed class ProductDiscountConfiguration
    : IEntityTypeConfiguration<ProductDiscount>
{
    public void Configure(EntityTypeBuilder<ProductDiscount> builder)
    {
        builder.ToTable("ProductDiscounts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Percentage)
            .IsRequired();

        builder.Property(x => x.ExpireDate)
            .IsRequired();

        builder.Property(x => x.UsedCount)
            .HasDefaultValue(0);

        builder.HasMany(x => x.ProductDiscountUses)
            .WithOne(x => x.ProductDiscount)
            .HasForeignKey(x => x.ProductDiscountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
