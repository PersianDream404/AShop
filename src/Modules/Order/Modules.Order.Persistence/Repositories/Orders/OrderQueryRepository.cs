using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Domain.Entities;
using Modules.Order.Domain.Enums;
using Modules.Order.Persistence.Context;
using Modules.Order.Persistence.Mapper.Orders;

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
                .Where(o => o.ShoppingCart.UserId == userId &&o.Status==OrderStatus.Pending)
                .ToList()
        );
    }

    public async Task<IEnumerable<OrderEntity>> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(
            _context.Orders
                .Where(o => o.ShoppingCart.SessionId == sessionId)
                .ToList()
        );
    }

    public async Task<IEnumerable<OrderDto>> GetAllProjectedAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .AsNoTracking()
            .Select(OrderMapper.ToGetAllDto())
            .ToListAsync(cancellationToken);
    }

    public async Task<OrderDto?> GetByIdProjectedAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(o => o.Id == id && o.Status == OrderStatus.Pending)
            .Select(OrderMapper.ToGetByIdDto())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OrderDto?> GetByUserIdProjectedAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(o => o.ShoppingCart.UserId == id && o.Status == OrderStatus.Pending)
            .Select(OrderMapper.ToGetByIdDto())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OrderDto?> GetBySessionIdProjectedAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(o => o.ShoppingCart.SessionId == id && o.Status == OrderStatus.Pending)
            .Select(OrderMapper.ToGetByIdDto())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OrderEntity?> GetByCartIdAsync(long cartId, CancellationToken cancellationToken = default)
    {
        return await
            _context.Orders
                .Where(o => o.ShoppingCartId == cartId && o.Status == OrderStatus.Pending)
                .FirstOrDefaultAsync(cancellationToken);
        
    }


    public async Task<decimal> GetTotalAmountByItemsAsync(long orderId)
    {
        var orderItems = await _context.OrderItems
            .Where(oi => oi.OrderId == orderId)
            .AsNoTracking()
            .Select(oi => new
            {
                oi.Quantity,
                oi.UnitPrice,
                //oi.DiscountAmount,
                //oi.DiscountPercent,
                //oi.DiscountType
            })
            .ToListAsync();

        return orderItems.Sum(item =>
        {
            decimal originalPrice = item.Quantity * item.UnitPrice;

            //if (!item.DiscountAmount.HasValue && !item.DiscountPercent.HasValue)
            //    return originalPrice;

            //if (item.DiscountType == DiscountType.Fixed)
            //    return Math.Max(0, originalPrice - item.DiscountValue.Value);

            //if (item.DiscountType == DiscountType.Percentage)
            //{
            //    var discountAmount = originalPrice * (item.DiscountValue.Value / 100m);
            // return Math.Max(0, originalPrice - discountAmount);
            //}
          //  return Math.Max(0, originalPrice - item.DiscountAmount!.Value);
              return originalPrice;
        });
    }
}
