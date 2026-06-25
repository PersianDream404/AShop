using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using Modules.Order.Domain.Entities;
using Modules.Order.Domain.Interfaces;

namespace Modules.Order.Application.UseCase.Orders.Commands;

public class AddOrderItemCommandHandler(IOrderCommandRepository commandRepository, IOrderQueryRepository queryRepository, IOrderItemCommandRepository itemRepository)
    : ICommandHandler<AddOrderItemCommand, bool>
{
    public async Task<Result<bool>> Handle(AddOrderItemCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await queryRepository.GetByIdAsync(command.OrderId, cancellationToken);
            if (order == null)
                return Result.Error(OrderValidationMessages.OrderNotFound);

            var itemResult = OrderItem.Create(
                command.OrderId,
                command.Request.ProductId,
                command.Request.UnitPrice,
                command.Request.Quantity,
                command.Request.DiscountValue
            );

            if (!itemResult.IsSuccess)
                return Result.Error(itemResult.Errors.FirstOrDefault() ?? OrderValidationMessages.ErrorAddingOrderItem);

            var addResult = order.AddOrderItem(itemResult.Value);
            if (!addResult.IsSuccess)
                return Result.Error(addResult.Errors.FirstOrDefault() ?? OrderValidationMessages.ErrorAddingOrderItem);

            await itemRepository.AddAsync(itemResult.Value, cancellationToken);
            await commandRepository.UpdateAsync(order, cancellationToken);
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            return Result.Error($"{OrderValidationMessages.ErrorAddingOrderItem}: {ex.Message}");
        }
    }
}

public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
{
    public AddOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);

        RuleFor(x => x.Request.ProductId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);

        RuleFor(x => x.Request.UnitPrice)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.GreaterThanZero);

        RuleFor(x => x.Request.Quantity)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.GreaterThanZero);
    }
}




