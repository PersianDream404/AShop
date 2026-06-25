using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Queries;

namespace Modules.Order.Application.UseCase.Orders.Queries;

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