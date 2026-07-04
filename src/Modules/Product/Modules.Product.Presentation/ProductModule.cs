using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Product.Persistence;
using Modules.Product.Presentation.Endpoints.Products.Write;
using SharedKernel.Interface;
using SmeOpsHub.SharedKernel;

namespace Modules.Product.Presentation;

public class ProductModule : IModule
{


    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityInfrastructure(configuration);
        services.AddIdentityApplication();

        services.Scan(scan =>
        {
            scan.FromAssemblyOf<CreateProductEndpoint.EndPoint>()
                .AddClasses(x => x.AssignableTo<IEndpoint>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime();
        });


    }
}
