using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using Modules.Order.Domain.Interfaces;

namespace Modules.Order.Application.UseCase.Orders.Commands;

public class UpdateTrackingNumberCommandHandler(IOrderCommandRepository commandRepository, IOrderQueryRepository queryRepository)
    : ICommandHandler<UpdateTrackingNumberCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateTrackingNumberCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await queryRepository.GetByIdAsync(command.Request.OrderId, cancellationToken);
            if (order == null)
                return Result.Error(OrderValidationMessages.OrderNotFound);

            var updateResult = order.UpdateTrackingNumber(command.Request.TrackingNumber);
            if (!updateResult.IsSuccess)
                return Result.Error(updateResult.Errors.FirstOrDefault()?.ErrorMessage ?? OrderValidationMessages.ErrorUpdatingTrackingNumber);

            await commandRepository.UpdateAsync(order, cancellationToken);
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            return Result.Error($"{OrderValidationMessages.ErrorUpdatingTrackingNumber}: {ex.Message}");
        }
    }
}








public class UpdateTrackingNumberCommandValidator : AbstractValidator<UpdateTrackingNumberCommand>
{
    public UpdateTrackingNumberCommandValidator()
    {
        RuleFor(x => x.Request.OrderId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);

        RuleFor(x => x.Request.TrackingNumber)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required)
            .MaximumLength(100)
            .WithMessage(SharedValidationMessages.MaxLength);
    }
}