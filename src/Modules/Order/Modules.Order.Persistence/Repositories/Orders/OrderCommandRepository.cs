using Infrastructure.Repositories;
using Modules.Order.Domain.Entities;
using Modules.Order.Domain.Interfaces;
using Modules.Order.Persistence.Context;

namespace Modules.Order.Persistence.Repositories.Orders;

public class OrderCommandRepository : CommandRepository<OrderEntity>, IOrderCommandRepository
{
    private readonly OrderWriteDbContext _context;

    public OrderCommandRepository(OrderWriteDbContext context) : base(context)
    {
        _context = context;
    }


}
