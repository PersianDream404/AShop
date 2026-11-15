using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Banner.Persistence.Context;

namespace Modules.Banner.Persistence.Extensions;

public static class BannerPersistenceExtensions
{
    public static async Task<WebApplication> ApplyBannerMigrationsAsync(this WebApplication app, CancellationToken cancellationToken = default)
    {
        using var scope = app.Services.CreateScope();

        var writeDbContext = scope.ServiceProvider.GetRequiredService<BannerWriteDbContext>();
        await writeDbContext.Database.MigrateAsync(cancellationToken);

        var readDbContext = scope.ServiceProvider.GetRequiredService<BannerReadDbContext>();
        await readDbContext.Database.MigrateAsync(cancellationToken);

        return app;
    }
}
