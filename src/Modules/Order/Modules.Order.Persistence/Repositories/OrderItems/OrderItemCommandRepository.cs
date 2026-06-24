using Infrastructure.Repositories;
using Modules.Order.Domain.Entities;
using Modules.Order.Domain.Interfaces;
using Modules.Order.Persistence.Context;

namespace Modules.Order.Persistence.Repositories.OrderItems;

public class OrderItemCommandRepository : CommandRepository<OrderItem>, IOrderItemCommandRepository
{
    private readonly OrderWriteDbContext _context;

    public OrderItemCommandRepository(OrderWriteDbContext context) : base(context)
    {
        _context = context;
    }


}
