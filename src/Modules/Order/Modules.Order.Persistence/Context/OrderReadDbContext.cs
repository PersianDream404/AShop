using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Modules.Order.Domain.Entities;

namespace Modules.Order.Persistence.Context;

public class OrderReadDbContext : BaseDbContext
{
    public OrderReadDbContext(DbContextOptions<BaseDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    #region DbSets
    
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderTransaction> OrderTransactions { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    #endregion
}
