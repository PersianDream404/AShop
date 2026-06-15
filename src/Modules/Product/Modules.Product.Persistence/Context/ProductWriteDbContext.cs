using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Modules.Product.Domain.Entities.Features;

namespace Modules.Product.Persistence.Context;
public class ProductWriteDbContext : BaseDbContext
{
    //public ProductWriteDbContext(DbContextOptions<ProductWriteDbContext> options) : base(options)
    //{
    //}

    public ProductWriteDbContext(DbContextOptions<BaseDbContext> options) : base(options)
    {
    
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
    #region DbSet
    public DbSet<Modules.Product.Domain.Entities.Products.Product> Products { get; set; }
    public DbSet<Modules.Product.Domain.Entities.Colors.Color> Colors { get; set; }
    public DbSet<Modules.Product.Domain.Entities.FeaturesCategories.FeaturesCategory> FeaturesCategory { get; set; }
    public DbSet<Modules.Product.Domain.Entities.Categories.Category> Category { get; set; }
    public DbSet<Modules.Product.Domain.Entities.Features.FeaturesValues> FeaturesValues { get; set; }


    public DbSet<ProductFeatures> ProductFeatures { get; set; }

    #endregion


}
