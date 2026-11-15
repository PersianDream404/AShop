using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Payment.Persistence.Context;

namespace Modules.Payment.Persistence.Extensions;

public static class PaymentPersistenceExtensions
{
    public static async Task<WebApplication> ApplyPaymentMigrationsAsync(this WebApplication app, CancellationToken cancellationToken = default)
    {
        using var scope = app.Services.CreateScope();

        var writeDbContext = scope.ServiceProvider.GetRequiredService<PaymentWriteDbContext>();
        await writeDbContext.Database.MigrateAsync(cancellationToken);

        var readDbContext = scope.ServiceProvider.GetRequiredService<PaymentReadDbContext>();
        await readDbContext.Database.MigrateAsync(cancellationToken);

        return app;
    }
}
