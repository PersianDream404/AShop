using Infrastructure.Repositories;
using Modules.Order.Domain.Entities;
using Modules.Order.Domain.Interfaces;
using Modules.Order.Persistence.Context;

namespace Modules.Order.Persistence.Repositories.ShoppingCarts;

public class ShoppingCartCommandRepository : CommandRepository<ShoppingCart>, IShoppingCartCommandRepository
{
    private readonly OrderWriteDbContext _context;

    public ShoppingCartCommandRepository(OrderWriteDbContext context) : base(context)
    {
        _context = context;
    }


}
