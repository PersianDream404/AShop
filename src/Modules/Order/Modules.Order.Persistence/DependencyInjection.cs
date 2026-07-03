using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.Interface.ShoppingCarts;
using Modules.Order.Domain.Interfaces;
using Modules.Order.Persistence.Context;
using Modules.Order.Persistence.Repositories.OrderItems;
using Modules.Order.Persistence.Repositories.Orders;
using Modules.Order.Persistence.Repositories.OrderTransactions;
using Modules.Order.Persistence.Repositories.ShoppingCarts;
using SharedKernel.Constants;

namespace Modules.Order.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddOrderInfrastructure(
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

        services.AddDbContext<OrderWriteDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDbContext<OrderReadDbContext>(options =>
            options.UseSqlServer(connectionString));

        #endregion

        #region Repositories

        services.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
        services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();

        
        services.AddScoped<IOrderTransactionCommandRepository, OrderTransactionCommandRepository>();
        services.AddScoped<IOrderTransactionQueryRepository, OrderTransactionQueryRepository>();
        
        
        services.AddScoped<IShoppingCartCommandRepository, ShoppingCartCommandRepository>();
        services.AddScoped<IShoppingCartQueryRepository, ShoppingCartQueryRepository>();

        services.AddScoped<IOrderItemQueryRepository, OrderItemQueryRepository>();
        services.AddScoped<IOrderItemCommandRepository, OrderItemCommandRepository>();

        #endregion

        return services;
    }
}
