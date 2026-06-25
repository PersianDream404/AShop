using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Domain.Entities;
using Modules.Order.Persistence.Context;

namespace Modules.Order.Persistence.Repositories.OrderItems;

public class OrderItemQueryRepository : QueryRepository<OrderItem>, IOrderItemQueryRepository
{
    private readonly OrderWriteDbContext _context;

    public OrderItemQueryRepository(OrderWriteDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<OrderItem?> GetAsync(long ProductId, long OrderId,CancellationToken cancellationToken=default)
    {
        return await _context.OrderItems.FirstOrDefaultAsync(x => x.OrderId == OrderId && x.ProductId == ProductId, cancellationToken);
    }
}
