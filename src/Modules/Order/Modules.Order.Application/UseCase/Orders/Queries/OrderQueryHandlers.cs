using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.UseCase.Orders.Queries;
using Modules.Order.Domain.Interfaces;

namespace Modules.Order.Application.UseCase.Orders.Queries;

public class GetOrderByIdQueryHandler(IOrderQueryRepository queryRepository)
    : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var order = await queryRepository.GetByIdAsync(query.OrderId, cancellationToken);
            if (order == null)
                return Result.Error("Order not found");

            return Result.Success(order.Adapt<OrderDto>());
        }
        catch (Exception ex)
        {
            return Result.Error($"Error retrieving order: {ex.Message}");
        }
    }
}

public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
{
    public GetOrderByIdQueryValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);
    }
}

public class GetOrdersByUserIdQueryHandler(IOrderQueryRepository queryRepository)
    : IQueryHandler<GetOrdersByUserIdQuery, IEnumerable<OrderDto>>
{
    public async Task<Result<IEnumerable<OrderDto>>> Handle(GetOrdersByUserIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var orders = await queryRepository.GetByUserIdAsync(query.UserId, cancellationToken);
            return Result.Success(orders.Adapt<IEnumerable<OrderDto>>());
        }
        catch (Exception ex)
        {
            return Result.Error($"Error retrieving orders: {ex.Message}");
        }
    }
}

public class GetOrdersByUserIdQueryValidator : AbstractValidator<GetOrdersByUserIdQuery>
{
    public GetOrdersByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);
    }
}

public class GetOrdersBySessionIdQueryHandler(IOrderQueryRepository queryRepository)
    : IQueryHandler<GetOrdersBySessionIdQuery, IEnumerable<OrderDto>>
{
    public async Task<Result<IEnumerable<OrderDto>>> Handle(GetOrdersBySessionIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var orders = await queryRepository.GetBySessionIdAsync(query.SessionId, cancellationToken);
            return Result.Success(orders.Adapt<IEnumerable<OrderDto>>());
        }
        catch (Exception ex)
        {
            return Result.Error($"Error retrieving orders: {ex.Message}");
        }
    }
}

public class GetOrdersBySessionIdQueryValidator : AbstractValidator<GetOrdersBySessionIdQuery>
{
    public GetOrdersBySessionIdQueryValidator()
    {
        RuleFor(x => x.SessionId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);
    }
}

public class GetAllOrdersQueryHandler(IOrderQueryRepository queryRepository)
    : IQueryHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{
    public async Task<Result<IEnumerable<OrderDto>>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var orders = await queryRepository.GetAllAsync(cancellationToken);
            return Result.Success(orders.Adapt<IEnumerable<OrderDto>>());
        }
        catch (Exception ex)
        {
            return Result.Error($"Error retrieving orders: {ex.Message}");
        }
    }
}
