using Infrastructure.Repositories;
using Modules.Product.Persistence.Context;
using Modules.Product.Domain.Interface;
using Modules.Product.Domain.Entities.Features;
using Modules.Product.Domain.Entities.Categories;
using Modules.Product.Domain.Interface.Categories;
namespace Modules.Product.Persistence.Repositories.Categorys;


public class CategoryCommandRepository
    : CommandRepository<Category>, ICategoryCommandRepository
{
    public CategoryCommandRepository(ProductWriteDbContext context) : base(context)
    {
    }

 
}
