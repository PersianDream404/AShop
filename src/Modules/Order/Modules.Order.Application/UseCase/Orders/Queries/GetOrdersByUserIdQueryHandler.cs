using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.PagedList;
using Microsoft.Extensions.Logging;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Queries;

namespace Modules.Order.Application.UseCase.Orders.Queries;

public class GetOrdersByUserIdQueryHandler(
    IOrderQueryRepository queryRepository,
    ILogger<GetOrdersByUserIdQueryHandler> logger)
    : IQueryHandler<GetOrdersByUserIdQuery, PagedList<OrderListItemDto>>
{
    public async Task<Result<PagedList<OrderListItemDto>>> Handle(
        GetOrdersByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            var orders = await queryRepository.GetByUserIdPagedAsync(
                query.UserId,
                query.PageNumber,
                query.PageSize,
                cancellationToken);
            return Result.Success(orders);
        }
        catch (Exception ex)
        {
            if (ex is OperationCanceledException)
                throw;

            logger.LogError(ex, "Failed to retrieve orders for user {UserId}", query.UserId);
            return Result.Error("در دریافت فهرست سفارش‌ها خطایی رخ داد.");
        }
    }
}

public class GetOrdersByUserIdQueryValidator : AbstractValidator<GetOrdersByUserIdQuery>
{
    public GetOrdersByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("شناسه کاربر نامعتبر است.");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("شماره صفحه باید بزرگ‌تر از صفر باشد.");

        RuleFor(x => x.PageNumber)
            .Must((query, pageNumber) => (long)(pageNumber - 1) * query.PageSize <= int.MaxValue)
            .WithMessage("شماره صفحه خارج از محدوده مجاز است.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("تعداد سفارش در هر صفحه باید بین ۱ تا ۱۰۰ باشد.");
    }
}