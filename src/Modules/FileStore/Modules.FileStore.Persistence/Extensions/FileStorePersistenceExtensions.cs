using Identity.Persistence.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.FileStore.Persistence.Context;

namespace Modules.FileStore.Persistence.Extensions;

public static class FileStorePersistenceExtensions
{
    public static async Task<WebApplication> ApplyFileStoreMigrationsAsync(this WebApplication app, CancellationToken cancellationToken = default)
    {
        using var scope = app.Services.CreateScope();

        var writeDbContext = scope.ServiceProvider.GetRequiredService<FileStoreWriteDbContext>();
        await writeDbContext.Database.MigrateAsync(cancellationToken);

        var readDbContext = scope.ServiceProvider.GetRequiredService<FileStoreReadDbContext>();
        await readDbContext.Database.MigrateAsync(cancellationToken);

        return app;
    }
}
