using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Modules.Payment.Domain.Entities;

namespace Modules.Payment.Persistence.Context;

public class PaymentReadDbContext : BaseDbContext
{
    public PaymentReadDbContext(DbContextOptions<BaseDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    #region DbSets
    
    public DbSet<PaymentEntity> Payment { get; set; }


    #endregion
}
