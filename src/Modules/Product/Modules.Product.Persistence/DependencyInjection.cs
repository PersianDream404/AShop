using Identity.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Product.Application.Contract.Interface.Brands;
using Modules.Product.Application.Contract.Interface.Products;
using Modules.Product.Domain.Interface.Brands;
using Modules.Product.Domain.Interface.Products;
using Modules.Product.Persistence.Context;
using Modules.Product.Persistence.Repositories.Brands;
using Modules.Product.Persistence.Repositories.Users;
using SharedKernel.Constants;

namespace Modules.Product.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
          .AddPersistence(configuration)
        ;

        return services;
    }

    private static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        #region AppSetting Bind

        //services.Configure<FarazSmsOptions>(options =>
        //    configuration.GetSection("FarazSms").Bind(options));

        //services.Configure<S3Configuration>(options =>
        //    configuration.GetSection("S3").Bind(options));
        #endregion


        services.AddDbContext<ProductWriteDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString(AppSetting.ConnectionString)));

        services.AddDbContext<ProductReadDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString(AppSetting.ConnectionString)));


        services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
        services.AddScoped<IProductQueryRepository, ProductQueryRepository>();



        services.AddScoped<IBrandCommandRepository, BrandCommandRepository>();
        services.AddScoped<IBrandQueryRepository, BrandQueryRepository>();


        return services;
    }







}