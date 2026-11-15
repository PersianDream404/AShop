using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Interface.ShoppingCarts;
using Modules.Order.Domain.Entities;
using Modules.Order.Persistence.Context;
using Modules.Order.Persistence.Mapper.ShoppingCarts;

namespace Modules.Order.Persistence.Repositories.ShoppingCarts;

public class ShoppingCartQueryRepository : QueryRepository<ShoppingCart>, IShoppingCartQueryRepository
{
    private readonly OrderReadDbContext _context;

    public ShoppingCartQueryRepository(OrderReadDbContext context) : base(context)
    {
        _context = context;
    }



    //public async Task<IEnumerable<ShoppingCartDto>> GetAllProjectedAsync(CancellationToken cancellationToken = default)
    //{
    //    return await _context.ShoppingCarts
    //        .AsNoTracking()
    //        .Select(ShoppingCartMapper.ToGetAllDto())
    //        .ToListAsync(cancellationToken);
    //}

    public async Task<ShoppingCartDto?> GetBySessionIdProjectedAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ShoppingCarts
            .AsNoTracking()
            .Where(s => s.SessionId == id)
            .Select(ShoppingCartMapper.ToGetByIdDto())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ShoppingCartDto?> GetByUserIdProjectedAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context.ShoppingCarts
            .AsNoTracking()
            .Where(s => s.UserId == id)
            .Select(ShoppingCartMapper.ToGetByIdDto())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ShoppingCart?> GetByNotIdAsync(long userId, long cartId, CancellationToken cancellationToken = default)
    {
        return await _context.ShoppingCarts
            .Where(x => x.UserId == userId && x.Id != cartId)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> AnyBySessionIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ShoppingCarts
            .AsNoTracking()
            .AnyAsync(s => s.SessionId == id,cancellationToken);
    }
}
