using Modules.Order.Application.Contract.DTOs;
using System.Linq.Expressions;

namespace Modules.Order.Persistence.Mapper.ShoppingCarts;

public static class ShoppingCartMapper
{
    public static Expression<Func<Domain.Entities.ShoppingCart, ShoppingCartDto>> ToGetByIdDto()
    {
        return x => new ShoppingCartDto
        {
            Id = x.Id,
            SessionId = x.SessionId,
            UserId = x.UserId,
            CreatedAt = x.CreatedAt
        };
    }

    public static Expression<Func<Domain.Entities.ShoppingCart, ShoppingCartDto>> ToGetAllDto()
    {
        return x => new ShoppingCartDto
        {
            Id = x.Id,
            SessionId = x.SessionId,
            UserId = x.UserId,
            CreatedAt = x.CreatedAt
        };
    }
}
