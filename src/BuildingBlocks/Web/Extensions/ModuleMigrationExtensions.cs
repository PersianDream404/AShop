using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Interface;

namespace Web.Extensions;

public static class ModuleMigrationExtensions
{
    public static async Task<WebApplication> ApplyModuleMigrationsAsync(
        this WebApplication app,
        CancellationToken cancellationToken = default)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var migrations = scope.ServiceProvider
            .GetServices<IModuleMigration>()
            .OrderBy(x => x.GetType().FullName, StringComparer.Ordinal);

        foreach (var migration in migrations)
            await migration.ApplyAsync(cancellationToken);

        return app;
    }
}
