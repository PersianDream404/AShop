using Identity.Persistence.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Product.Persistence.Context;

namespace Modules.Product.Persistence.Extensions;

public static class ProductPersistenceExtensions
{
    public static async Task<WebApplication> ApplyProductMigrationsAsync(this WebApplication app, CancellationToken cancellationToken = default)
    {
        using var scope = app.Services.CreateScope();

        var writeDbContext = scope.ServiceProvider.GetRequiredService<ProductWriteDbContext>();
        await writeDbContext.Database.MigrateAsync(cancellationToken);

        var readDbContext = scope.ServiceProvider.GetRequiredService<ProductReadDbContext>();
        await readDbContext.Database.MigrateAsync(cancellationToken);

        return app;
    }
}
