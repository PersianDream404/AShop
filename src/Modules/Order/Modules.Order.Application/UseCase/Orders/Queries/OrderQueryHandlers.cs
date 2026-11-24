using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.Validation.Resources;
using Microsoft.Extensions.Logging;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Queries;
using Modules.Order.Domain.Interfaces;

namespace Modules.Order.Application.UseCase.Orders.Queries;

public class GetOrderByIdQueryHandler(
    IOrderQueryRepository queryRepository,
    ILogger<GetOrderByIdQueryHandler> logger)
    : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var order = await queryRepository.GetByUserAndOrderIdProjectedAsync(
                query.UserId,
                query.OrderId,
                cancellationToken);
            if (order == null)
                return Result.NotFound(OrderValidationMessages.OrderNotFound);

            return Result.Success(order);
        }
        catch (Exception ex)
        {
            if (ex is OperationCanceledException)
                throw;

            logger.LogError(ex, "Failed to retrieve order {OrderId} for user {UserId}", query.OrderId, query.UserId);
            return Result.Error("در دریافت جزئیات سفارش خطایی رخ داد.");
        }
    }
}

public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
{
    public GetOrderByIdQueryValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.Invalid);

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.Invalid);
    }
}
