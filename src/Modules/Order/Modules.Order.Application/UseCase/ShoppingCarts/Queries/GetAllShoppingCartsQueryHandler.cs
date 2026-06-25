using Ardalis.Result;
using Framwork.Bus.Query;
using Mapster;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Interface.ShoppingCarts;
using Modules.Order.Application.Contract.UseCase.ShoppingCarts.Queries;

namespace Modules.Order.Application.UseCase.ShoppingCarts.Queries;

public class GetAllShoppingCartsQueryHandler(IShoppingCartQueryRepository queryRepository)
    : IQueryHandler<GetAllShoppingCartsQuery, IEnumerable<ShoppingCartDto>>
{
    public async Task<Result<IEnumerable<ShoppingCartDto>>> Handle(GetAllShoppingCartsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var carts = await queryRepository.GetAllAsync(cancellationToken);
            return Result.Success(carts.Adapt<IEnumerable<ShoppingCartDto>>());
        }
        catch (Exception ex)
        {
            return Result.Error($"Error retrieving shopping carts: {ex.Message}");
        }
    }
}
