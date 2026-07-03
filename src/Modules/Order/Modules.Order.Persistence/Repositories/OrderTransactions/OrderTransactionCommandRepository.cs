using Infrastructure.Repositories;
using Modules.Order.Domain.Entities;
using Modules.Order.Domain.Interfaces;
using Modules.Order.Persistence.Context;

namespace Modules.Order.Persistence.Repositories.OrderItems;

public class OrderTransactionCommandRepository : CommandRepository<OrderTransaction>, IOrderTransactionCommandRepository
{
    private readonly OrderWriteDbContext _context;

    public OrderTransactionCommandRepository(OrderWriteDbContext context) : base(context)
    {
        _context = context;
    }


}