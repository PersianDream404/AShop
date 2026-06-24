using Infrastructure.Repositories;
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


}
