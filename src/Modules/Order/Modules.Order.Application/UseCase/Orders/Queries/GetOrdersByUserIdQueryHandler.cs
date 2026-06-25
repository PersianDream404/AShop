using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Queries;

namespace Modules.Order.Application.UseCase.Orders.Queries;

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