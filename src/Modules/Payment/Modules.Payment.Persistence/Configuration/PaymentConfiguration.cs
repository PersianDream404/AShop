using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Payment.Domain.Entities;
using SharedKernel.Base;

namespace Modules.Order.Persistence.Configuration;

public class PaymentConfiguration : IEntityTypeConfiguration<PaymentEntity>
{
    public void Configure(EntityTypeBuilder<PaymentEntity> entity)
    {
        entity.ToTable("Payments");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Amount)
            .HasPrecision(18, 2);

        entity.Property(x => x.FailedReturnUrl)
            .HasMaxLength(1000);
        
        entity.Property(x => x.SuccessReturnUrl)
            .HasMaxLength(1000);

        entity.Property(x => x.TransactionCode)
            .HasMaxLength(128);

        entity.HasIndex(x => x.TrackingNumber);

    }
}
