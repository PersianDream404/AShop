using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Order.Domain.Entities;

namespace Modules.Order.Persistence.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ShoppingCartId)
            .IsRequired();

        builder.Property(x => x.TotalAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.TaxAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.TermsAccepted)
            .IsRequired();

        builder.Property(x => x.ShippingAddress)
            .HasMaxLength(500);

        builder.Property(x => x.MobileNumber)
            .HasMaxLength(20);

        builder.Property(x => x.TrackingNumber)
            .HasMaxLength(100);

        builder.Property(x => x.DisplayId)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        // Relationships
        builder.HasOne(x => x.ShoppingCart)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.ShoppingCartId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.OrderItems)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
