using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.Interface.ShoppingCarts;
using Modules.Order.Domain.Entities;
using Modules.Order.Persistence.Context;

namespace Modules.Order.Persistence.Repositories.OrderTransactions;

public class OrderTransactionQueryRepository : QueryRepository<OrderTransaction>, IOrderTransactionQueryRepository
{
    private readonly OrderWriteDbContext _context;

    public OrderTransactionQueryRepository(OrderWriteDbContext context) : base(context)
    {
        _context = context;
    }

 
}
