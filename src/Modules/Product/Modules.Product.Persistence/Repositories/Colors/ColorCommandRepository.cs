using Infrastructure.Repositories;
using Modules.Product.Domain.Interface.Colors;
using Modules.Product.Persistence.Context;
using Modules.Product.Domain.Entities.Colors;
namespace Modules.Product.Persistence.Repositories.Colors;


public class ColorCommandRepository
    : CommandRepository<Modules.Product.Domain.Entities.Colors.Color>, IColorCommandRepository
{
    public ColorCommandRepository(ProductWriteDbContext context) : base(context)
    {
    }

 
}
