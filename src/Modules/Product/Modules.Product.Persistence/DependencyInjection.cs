using Identity.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Product.Application.Contract.Interface.Brands;
using Modules.Product.Application.Contract.Interface.Categories;
using Modules.Product.Application.Contract.Interface.Colors;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.Interface.FeaturesCategories;
using Modules.Product.Application.Contract.Interface.Products;
using Modules.Product.Domain.Interface;
using Modules.Product.Domain.Interface.Brands;
using Modules.Product.Domain.Interface.Categories;
using Modules.Product.Domain.Interface.Colors;
using Modules.Product.Domain.Interface.FeaturesCategories;
using Modules.Product.Domain.Interface.Products;
using Modules.Product.Persistence.Context;
using Modules.Product.Persistence.Repositories.Brands;
using Modules.Product.Persistence.Repositories.Categorys;
using Modules.Product.Persistence.Repositories.Colors;
using Modules.Product.Persistence.Repositories.FeaturesCategorys;
using Modules.Product.Persistence.Repositories.FeaturesValuess;
using Modules.Product.Persistence.Repositories.ProductFeaturess;
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

        services.AddScoped<IColorCommandRepository, ColorCommandRepository>();
        services.AddScoped<IColorQueryRepository, ColorQueryRepository>();


        services.AddScoped<IFeaturesCategoryCommandRepository, FeaturesCategoryCommandRepository>();
        services.AddScoped<IFeaturesCategoryQueryRepository, FeaturesCategoryQueryRepository>();


        services.AddScoped<IProductFeaturesQueryRepository, ProductFeaturesQueryRepository>();
        services.AddScoped<IProductFeaturesCommandRepository, ProductFeaturesCommandRepository>();



        services.AddScoped<ICategoryCommandRepository, CategoryCommandRepository>();
        services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();


        services.AddScoped<IFeaturesValuesCommandRepository, FeaturesValuesCommandRepository>();
        services.AddScoped<IFeaturesValuesQueryRepository, FeaturesValuesQueryRepository>();

        return services;
    }







}