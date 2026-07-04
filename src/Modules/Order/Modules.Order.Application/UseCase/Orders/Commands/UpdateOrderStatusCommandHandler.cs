using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using Modules.Order.Domain.Interfaces;

namespace Modules.Order.Application.UseCase.Orders.Commands;

public class UpdateOrderStatusCommandHandler(IOrderCommandRepository commandRepository, IOrderQueryRepository queryRepository)
    : ICommandHandler<UpdateOrderStatusCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await queryRepository.GetByIdAsync(command.Request.OrderId, cancellationToken);
            if (order == null)
                return Result.Error(OrderValidationMessages.OrderNotFound);

            var statusResult = order.UpdateStatus(command.Request.NewStatus);
            if (!statusResult.IsSuccess)
                return Result.Error(statusResult.Errors.FirstOrDefault() ?? OrderValidationMessages.ErrorUpdatingOrderStatus);

            await commandRepository.UpdateAsync(order, cancellationToken);
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            return Result.Error($"{OrderValidationMessages.ErrorUpdatingOrderStatus}: {ex.Message}");
        }
    }
}






public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusCommandValidator()
    {
        RuleFor(x => x.Request.OrderId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.Invalid);

        RuleFor(x => x.Request.NewStatus)
            .IsInEnum()
            .WithMessage(OrderValidationMessages.InvalidOrderStatus);
    }
}




