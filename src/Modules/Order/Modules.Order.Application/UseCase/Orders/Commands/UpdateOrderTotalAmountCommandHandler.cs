using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using Modules.Order.Domain.Interfaces;

namespace Modules.Order.Application.UseCase.Orders.Commands;

public class UpdateOrderTotalAmountCommandHandler(IOrderCommandRepository commandRepository, IOrderQueryRepository queryRepository)
    : ICommandHandler<UpdateOrderTotalAmountCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateOrderTotalAmountCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await queryRepository.GetByIdAsync(command.OrderId, cancellationToken);
            if (order == null)
                return Result.Error(OrderValidationMessages.OrderNotFound);

            var newTotalAmount =await queryRepository.GetTotalAmountByItemsAsync(command.OrderId);
            var amountResult = order.UpdateTotalAmount(newTotalAmount);
            if (!amountResult.IsSuccess)
                return Result.Error(amountResult.Errors.FirstOrDefault() ?? OrderValidationMessages.ErrorUpdatingOrderTotalAmount);

            await commandRepository.UpdateAsync(order, cancellationToken);
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            return Result.Error($"{OrderValidationMessages.ErrorUpdatingOrderTotalAmount}: {ex.Message}");
        }
    }
}

public class UpdateOrderTotalAmountCommandValidator : AbstractValidator<UpdateOrderTotalAmountCommand>
{
    public UpdateOrderTotalAmountCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);

        //RuleFor(x => x.NewTotalAmount)
        //    .GreaterThanOrEqualTo(0)
        //    .WithMessage(SharedValidationMessages.GreaterThanZero);
    }
}