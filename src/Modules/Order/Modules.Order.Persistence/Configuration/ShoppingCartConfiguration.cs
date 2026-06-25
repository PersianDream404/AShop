using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Order.Domain.Entities;

namespace Modules.Order.Persistence.Configuration;

public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.ToTable("ShoppingCarts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SessionId)

            .IsRequired();

        builder.Property(x => x.UserId);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        // Relationships
        builder.HasMany(x => x.Orders)
            .WithOne(x => x.ShoppingCart)
            .HasForeignKey(x => x.ShoppingCartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
