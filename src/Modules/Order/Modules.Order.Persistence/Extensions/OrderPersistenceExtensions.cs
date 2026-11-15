using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Order.Persistence.Context;

namespace Modules.Order.Persistence.Extensions;

public static class OrderPersistenceExtensions
{
    public static async Task<WebApplication> ApplyOrderMigrationsAsync(this WebApplication app, CancellationToken cancellationToken = default)
    {
        using var scope = app.Services.CreateScope();

        var writeDbContext = scope.ServiceProvider.GetRequiredService<OrderWriteDbContext>();
        await writeDbContext.Database.MigrateAsync(cancellationToken);

        var readDbContext = scope.ServiceProvider.GetRequiredService<OrderReadDbContext>();
        await readDbContext.Database.MigrateAsync(cancellationToken);

        return app;
    }
}
