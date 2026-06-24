using Infrastructure.Repositories;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Domain.Entities;
using Modules.Order.Persistence.Context;

namespace Modules.Order.Persistence.Repositories.Orders;

public class OrderQueryRepository : QueryRepository<OrderEntity>, IOrderQueryRepository
{
    private readonly OrderReadDbContext _context;

    public OrderQueryRepository(OrderReadDbContext context) : base(context)
    {
        _context = context;
    }


    public async Task<IEnumerable<OrderEntity>> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(
            _context.Orders
                .Where(o => o.ShoppingCart.UserId == userId)
                .ToList()
        );
    }

    public async Task<IEnumerable<OrderEntity>> GetBySessionIdAsync(long sessionId, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(
            _context.Orders
                .Where(o => o.ShoppingCart.SessionId == sessionId)
                .ToList()
        );
    }


}
