using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Modules.Product.Persistence.Context;
public class WriteDbContext : BaseDbContext
{
    public WriteDbContext(DbContextOptions<BaseDbContext> options) : base(options)
    {
    
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
    #region DbSet
    public DbSet<Modules.Product.Domain.Entities.Products.Product> Products { get; set; }


    #endregion


}
