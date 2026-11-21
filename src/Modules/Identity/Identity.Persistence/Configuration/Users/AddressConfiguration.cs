using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.Configuration.Users;

public sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.RecipientName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Province).IsRequired().HasMaxLength(100);
        builder.Property(x => x.City).IsRequired().HasMaxLength(100);
        builder.Property(x => x.PostalCode).IsRequired().HasMaxLength(20);
        builder.Property(x => x.AddressLine).IsRequired().HasMaxLength(500);
        builder.Property(x => x.MobileNumber).IsRequired().HasMaxLength(20);
        builder.Property(x => x.IsDefault).IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Addresses)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.UserId, x.Deleted, x.IsDefault });
    }
}
