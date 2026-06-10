using Modules.Product.Domain.Entities.Categories;
using SharedKernel.Interface.Repositories;


namespace Modules.Product.Domain.Interface.Categories;

public interface IProductCategoryQueryRepository : IQueryRepository<ProductCategory>
{
}
