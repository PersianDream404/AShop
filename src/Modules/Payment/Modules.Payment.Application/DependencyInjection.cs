using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Bus.Query;
using Framwork.Decorator.Command;
using Framwork.Decorator.Query;
using Framwork.Extensions;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Modules.Payment.Application.Contract.Service;
using Modules.Payment.Application.Services;
using Parbad.Builder;
using Parbad.Gateway.ZarinPal;
using SharedKernel.Events;

namespace Modules.Payment.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddPaymentApplication(
        this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()

            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()

            .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        FluentValidationConfig.Configure();

        services.AddScoped(typeof(IQueryBehavior<,>), typeof(LoggingQueryBehavior<,>));
        services.AddScoped(typeof(IQueryBehavior<,>), typeof(ValidationQueryBehavior<,>));

        services.AddScoped(typeof(ICommandBehavior<,>), typeof(LoggingCommandBehavior<,>));
        services.AddScoped(typeof(ICommandBehavior<,>), typeof(ValidationCommandBehavior<,>));

        services.AddScoped<ICommandBus, CommandBus>();
        services.AddScoped<IQueryBus, QueryBus>();

        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IPaymentCallbackUrlFactory, PaymentCallbackUrlFactory>();
        #region Mapping

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(assembly);
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        #endregion

        services.AddScoped<IEventBus, MediatREventBus>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(PaymentService).Assembly);
        });

         services.AddParbad()
           .ConfigureGateways(gateways =>
           {
               gateways.AddZarinPal()
                   .WithAccounts(acc =>
                   {
                       acc.AddInMemory(m =>
                       {
                           m.MerchantId = "ee4381fd-1884-4d62-a4e5-f3081def03e2";
                           m.IsSandbox = true;
                       });
                   });
           })
           .ConfigureHttpContext(httpContext => httpContext.UseDefaultAspNetCore())
           .ConfigureStorage(storage => storage.UseMemoryCache());

        return services;
    }
}
