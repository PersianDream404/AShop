using Infrastructure.Repositories;
using Modules.Product.Domain.Interface.Brands;
using Modules.Product.Persistence.Context;
using Modules.Product.Domain.Entities.Brands;
namespace Modules.Product.Persistence.Repositories.Brands;


public class BrandCommandRepository
    : CommandRepository<Modules.Product.Domain.Entities.Brands.Brand>, IBrandCommandRepository
{
    public BrandCommandRepository(ProductWriteDbContext context) : base(context)
    {
    }

 
}
