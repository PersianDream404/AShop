namespace Modules.Product.Persistence.Configuration.Colors;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Product.Domain.Entities.Colors;

public sealed class ProductColorConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.ToTable("ProductColors");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ColorName)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.ColorCode)
            .IsRequired()
            .HasMaxLength(250);

        //builder.Property(x => x.Price)
        //    .IsRequired();

        //builder.HasOne(x => x.Product)
        //    .WithMany(x => x.ProductColors)
        //    .HasForeignKey(x => x.ProductId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasIndex(x => new
        //{
        //    x.ProductId,
        //    x.ColorName
        //});

        //builder.HasIndex(x => new
        //{
        //    x.ProductId,
        //    x.ColorCode
        //});
    }
}
