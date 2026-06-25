using Ardalis.Result;
using Framwork.Bus.Query;
using Mapster;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Interface.ShoppingCarts;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.ShoppingCarts.Queries;

namespace Modules.Order.Application.UseCase.ShoppingCarts.Queries;

public class GetShoppingCartByUserIdQueryHandler(IShoppingCartQueryRepository queryRepository)
    : IQueryHandler<GetShoppingCartByUserIdQuery, ShoppingCartDto>
{
    public async Task<Result<ShoppingCartDto>> Handle(GetShoppingCartByUserIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await queryRepository.GetByUserIdAsync(query.UserId, cancellationToken);
            if (cart == null)
                return Result.Error(OrderValidationMessages.ShoppingCartNotFound);

            return Result.Success(cart.Adapt<ShoppingCartDto>());
        }
        catch (Exception ex)
        {
            return Result.Error($"Error retrieving shopping cart: {ex.Message}");
        }
    }
}
