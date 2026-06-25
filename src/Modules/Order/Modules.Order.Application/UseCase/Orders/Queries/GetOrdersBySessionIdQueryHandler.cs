using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Bus.Query;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Queries;

namespace Modules.Order.Application.UseCase.Orders.Queries;

public class GetOrdersBySessionIdQueryHandler(IOrderQueryRepository queryRepository)
    : IQueryHandler<GetOrdersBySessionIdQuery, OrderDto>
{
    public async Task<Result<OrderDto>> Handle(GetOrdersBySessionIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var order = await queryRepository.GetBySessionIdProjectedAsync(query.SessionId, cancellationToken);
            if (order == null)
                return Result.Error(OrderValidationMessages.OrderNotFound);

            return Result.Success(order);
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
            .NotEmpty()
            .WithMessage(SharedValidationMessages.InvalidId);
    }
}