using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Order.Application.Contract.Interface.ShoppingCarts;
using Modules.Order.Domain.Entities;
using Modules.Order.Persistence.Context;

namespace Modules.Order.Persistence.Repositories.ShoppingCarts;

public class ShoppingCartQueryRepository : QueryRepository<ShoppingCart>, IShoppingCartQueryRepository
{
    private readonly OrderReadDbContext _context;

    public ShoppingCartQueryRepository(OrderReadDbContext context) : base(context)
    {
        _context = context;
    }



    public async Task<ShoppingCart> GetBySessionIdAsync(long sessionId, CancellationToken cancellationToken = default)
    {
        return await _context.ShoppingCarts
            .FirstOrDefaultAsync(s => s.SessionId == sessionId, cancellationToken);
    }

    public async Task<ShoppingCart> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        return await _context.ShoppingCarts
            .FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);
    }
}
