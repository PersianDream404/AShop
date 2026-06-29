using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Queries;
using Modules.Order.Domain.Interfaces;

namespace Modules.Order.Application.UseCase.Orders.Queries;

public class GetPaymentSummaryByIdQueryHandler(IOrderQueryRepository queryRepository)
    : IQueryHandler<GetPaymentSummaryByIdQuery, GetPaymentSummaryOrderDto>
{
    public async Task<Result<GetPaymentSummaryOrderDto>> Handle(GetPaymentSummaryByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var order = await queryRepository.GetPaymentSummaryByIdProjectedAsync(query.OrderId, cancellationToken);
            if (order == null)
                return Result.Error(OrderValidationMessages.OrderNotFound);

            return Result.Success(order);
        }
        catch (Exception ex)
        {
            return Result.Error($"Error retrieving order: {ex.Message}");
        }
    }
}

public class GetPaymentSummaryByIdQueryValidator : AbstractValidator<GetPaymentSummaryByIdQuery>
{
    public GetPaymentSummaryByIdQueryValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);
    }
}
