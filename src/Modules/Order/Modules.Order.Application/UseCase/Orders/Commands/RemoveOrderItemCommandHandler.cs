using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using Modules.Order.Domain.Interfaces;

namespace Modules.Order.Application.UseCase.Orders.Commands;

public class RemoveOrderItemCommandHandler(IOrderCommandRepository commandRepository, IOrderQueryRepository orderQueryRepository,  IOrderItemQueryRepository itemRepository, IOrderItemCommandRepository orderItemCommandRepository)
    : ICommandHandler<RemoveOrderItemCommand, bool>
{
    public async Task<Result<bool>> Handle(RemoveOrderItemCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var orderItem = await itemRepository.GetByIdAsync(command.OrderItemId, cancellationToken);
            if (orderItem == null)
                return Result.Error(OrderValidationMessages.OrderItemNotFound);

            var order = await orderQueryRepository.GetByIdAsync(orderItem.OrderId, cancellationToken);
            if (order == null)
                return Result.Error(OrderValidationMessages.OrderNotFound);

            var removeResult = order.RemoveOrderItem(orderItem);
            if (!removeResult.IsSuccess)
                return Result.Error(removeResult.Errors.FirstOrDefault() ?? OrderValidationMessages.ErrorRemovingOrderItem);

            await orderItemCommandRepository.DeleteAsync(command.OrderItemId, cancellationToken);
            await commandRepository.UpdateAsync(order, cancellationToken);
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            return Result.Error($"{OrderValidationMessages.ErrorRemovingOrderItem}: {ex.Message}");
        }
    }
}






public class RemoveOrderItemCommandValidator : AbstractValidator<RemoveOrderItemCommand>
{
    public RemoveOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderItemId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);
    }
}