using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Mapster;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.ShoppingCarts.Queries;
using Modules.Order.Domain.Interfaces;

namespace Modules.Order.Application.UseCase.ShoppingCarts.Queries;

public class GetShoppingCartByIdQueryHandler(IShoppingCartQueryRepository queryRepository)
    : IQueryHandler<GetShoppingCartByIdQuery, ShoppingCartDto>
{
    public async Task<Result<ShoppingCartDto>> Handle(GetShoppingCartByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await queryRepository.GetByIdAsync(query.CartId, cancellationToken);
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

public class GetShoppingCartBySessionIdQueryHandler(IShoppingCartQueryRepository queryRepository)
    : IQueryHandler<GetShoppingCartBySessionIdQuery, ShoppingCartDto>
{
    public async Task<Result<ShoppingCartDto>> Handle(GetShoppingCartBySessionIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await queryRepository.GetBySessionIdAsync(query.SessionId, cancellationToken);
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
