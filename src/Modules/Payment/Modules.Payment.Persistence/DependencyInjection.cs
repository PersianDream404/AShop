using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Order.Domain.Interfaces;
using Modules.Payment.Application.Contract.Interface;
using Modules.Payment.Application.Contract.Service;
using Modules.Payment.Persistence.Context;
using Modules.Payment.Persistence.Repositories.Payments;
using SharedKernel.Constants;

namespace Modules.Payment.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPaymentInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddPersistence(configuration);

        return services;
    }

    private static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        #region DbContext

        var connectionString = configuration.GetConnectionString(AppSetting.ConnectionString)
                               ?? throw new InvalidOperationException($"Connection string '{AppSetting.ConnectionString}' not found.");

        services.AddDbContext<PaymentWriteDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDbContext<PaymentReadDbContext>(options =>
            options.UseSqlServer(connectionString));

        #endregion

        #region Repositories

        services.AddScoped<IPaymentQueryRepository, PaymentQueryRepository>();
        services.AddScoped<IPaymentCommandRepository, PaymentCommandRepository>();




        #endregion

        return services;
    }
}
