using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Payment.Application;
using Modules.Payment.Persistence;
using Modules.Payment.Presentation.Endpoints.Payments.Read;
using SharedKernel.Interface;
namespace Modules.Payment.Presentation;

public class PaymentModule : IModule
{
    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddPaymentInfrastructure(configuration);
        services.AddPaymentApplication();

        services.Scan(scan =>
        {
            scan.FromAssemblyOf<GetPaymentByIdEndpoint.EndPoint>()
                .AddClasses(x => x.AssignableTo<IEndpoint>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime();
        });
    }
}
