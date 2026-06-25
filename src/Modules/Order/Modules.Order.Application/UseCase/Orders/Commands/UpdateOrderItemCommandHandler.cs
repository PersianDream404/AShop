using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using Modules.Order.Domain.Interfaces;

namespace Modules.Order.Application.UseCase.Orders.Commands;

public class UpdateOrderItemCommandHandler(IOrderCommandRepository commandRepository, IOrderQueryRepository orderQueryRepository, IOrderItemQueryRepository itemRepository,IOrderItemCommandRepository orderItemCommandRepository)
    : ICommandHandler<UpdateOrderItemCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateOrderItemCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await orderQueryRepository.GetByIdAsync(command.OrderId, cancellationToken);
            if (order == null)
                return Result.Error(OrderValidationMessages.OrderNotFound);

            var orderItem = await itemRepository.GetByIdAsync(command.Request.Id, cancellationToken);
            if (orderItem == null)
                return Result.Error(OrderValidationMessages.OrderItemNotFound);

            var updateResult = order.UpdateOrderItem(orderItem, command.Request.Quantity);
            if (!updateResult.IsSuccess)
                return Result.Error(updateResult.Errors.FirstOrDefault() ?? OrderValidationMessages.ErrorUpdatingOrderItem);

            await orderItemCommandRepository.UpdateAsync(orderItem, cancellationToken);
            await commandRepository.UpdateAsync(order, cancellationToken);
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            return Result.Error($"{OrderValidationMessages.ErrorUpdatingOrderItem}: {ex.Message}");
        }
    }
}



public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
{
    public UpdateOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);

        RuleFor(x => x.Request.Id)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);

        RuleFor(x => x.Request.Quantity)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.GreaterThanZero);
    }
}