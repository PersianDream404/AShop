using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Modules.Product.Domain.Entities.Features;
using SharedKernel.Base;
using System.Linq.Expressions;

namespace Identity.Persistence.Context;
public class ProductReadDbContext : BaseDbContext
{
    public ProductReadDbContext(DbContextOptions<BaseDbContext> options) : base(options)
    {
    
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
    #region DbSet


    public DbSet<Modules.Product.Domain.Entities.Products.Product> Products { get; set; }
    public DbSet<Modules.Product.Domain.Entities.Brands.Brand> Brands { get; set; }
    public DbSet<Modules.Product.Domain.Entities.Colors.Color> Colors { get; set; }
    public DbSet<Modules.Product.Domain.Entities.FeaturesCategories.FeaturesCategory> FeaturesCategory { get; set; }
    public DbSet<ProductFeatures> ProductFeatures { get; set; }





    #endregion


}
