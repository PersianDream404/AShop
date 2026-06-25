using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Order.Application;
using Modules.Order.Persistence;
using Modules.Order.Presentation.Endpoints.Orders.Write;
using SharedKernel.Interface;
using Modules.Order.Application;
namespace Modules.Order.Presentation;

public class OrderModule : IModule
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOrderInfrastructure(configuration);
        services.AddOrderApplication();

        services.Scan(scan =>
        {
            scan.FromAssemblyOf<CreateOrderEndpoint.EndPoint>()
                .AddClasses(x => x.AssignableTo<IEndpoint>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime();
        });
    }
}
