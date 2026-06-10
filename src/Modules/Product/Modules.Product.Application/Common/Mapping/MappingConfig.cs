namespace Identity.Application.Common.Mapping;

using Mapster;
using Modules.Product.Application.Contract.DTOs.Products.Create;
using Modules.Product.Domain.Entities.Discounts;
using Modules.Product.Domain.Entities.Products;
using SharedKernel.Constants;

public static class MappingConfig
{
    public static void RegisterMappings()
    {
        #region Products

        TypeAdapterConfig<CreateProductRequestDto, Product>.NewConfig()
             .Map(dest => dest.ProductSelectedCategories, src => src.CategoriesIds.Select(x=>new ProductSelectedCategory { ProductCategoryId=x}))
             .Map(dest => dest.ProductDiscounts, src => src.DiscountsIds.Select(x=>new ProductDiscountUse { ProductDiscountId=x}))
             .Map(dest => dest.ProductSelectedColors, src => src.SelectedColorsIds.Select(x=>new ProductSelectedColors { ProductColorId=x}))
            ;
        #endregion

    }
}
