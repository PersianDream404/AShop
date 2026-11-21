using Microsoft.EntityFrameworkCore;
using SharedKernel.Interface;

namespace Infrastructure.Migrations;

public sealed class DbContextMigration<TContext>(TContext context) : IModuleMigration
    where TContext : DbContext
{
    public Task ApplyAsync(CancellationToken cancellationToken = default)
        => context.Database.MigrateAsync(cancellationToken);
}
