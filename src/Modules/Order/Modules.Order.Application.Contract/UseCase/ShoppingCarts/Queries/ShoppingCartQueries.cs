using Framwork.Bus.Query;
using Modules.Order.Application.Contract.DTOs;

namespace Modules.Order.Application.Contract.UseCase.ShoppingCarts.Queries;

public record GetShoppingCartByIdQuery(long CartId) : IQuery<ShoppingCartDto>;
public record GetShoppingCartBySessionIdQuery(long SessionId) : IQuery<ShoppingCartDto>;
public record GetShoppingCartByUserIdQuery(long UserId) : IQuery<ShoppingCartDto>;
public record GetAllShoppingCartsQuery() : IQuery<IEnumerable<ShoppingCartDto>>;
