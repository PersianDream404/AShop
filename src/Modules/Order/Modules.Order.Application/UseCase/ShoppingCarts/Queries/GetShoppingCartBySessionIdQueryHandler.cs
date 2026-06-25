using Ardalis.Result;
using Framwork.Bus.Query;
using Mapster;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Interface.ShoppingCarts;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.ShoppingCarts.Queries;

namespace Modules.Order.Application.UseCase.ShoppingCarts.Queries;

public class GetShoppingCartBySessionIdQueryHandler(IShoppingCartQueryRepository queryRepository)
    : IQueryHandler<GetShoppingCartBySessionIdQuery, ShoppingCartDto>
{
    public async Task<Result<ShoppingCartDto>> Handle(GetShoppingCartBySessionIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await queryRepository.GetBySessionIdProjectedAsync(query.SessionId, cancellationToken);
            if (cart == null)
                return Result.Error(OrderValidationMessages.ShoppingCartNotFound);

            return Result.Success(cart);
        }
        catch (Exception ex)
        {
            return Result.Error($"Error retrieving shopping cart: {ex.Message}");
        }
    }
}
