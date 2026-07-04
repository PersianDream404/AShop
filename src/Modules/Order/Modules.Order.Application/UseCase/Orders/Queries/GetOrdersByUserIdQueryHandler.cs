using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Queries;

namespace Modules.Order.Application.UseCase.Orders.Queries;

public class GetOrdersByUserIdQueryHandler(IOrderQueryRepository queryRepository)
    : IQueryHandler<GetOrdersByUserIdQuery, OrderDto>
{
    public async Task<Result<OrderDto>> Handle(GetOrdersByUserIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var order = await queryRepository.GetByUserIdProjectedAsync(query.UserId, cancellationToken);
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
public class GetOrdersByUserIdQueryValidator : AbstractValidator<GetOrdersByUserIdQuery>
{
    public GetOrdersByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.Invalid);
    }
}