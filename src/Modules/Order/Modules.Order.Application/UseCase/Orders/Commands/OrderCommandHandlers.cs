using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using Modules.Order.Domain.Entities;
using Modules.Order.Domain.Enums;
using Modules.Order.Domain.Interfaces;
using SharedKernel.Constants;
using SharedKernel.Helper;
using System.Text.RegularExpressions;

namespace Modules.Order.Application.UseCase.Orders.Commands;

public class CreateOrderCommandHandler(IOrderCommandRepository commandRepository, IShoppingCartQueryRepository cartQueryRepository)
    : ICommandHandler<CreateOrderCommand, bool>
{
    public async Task<Result<bool>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await cartQueryRepository.GetByIdAsync(command.Request.ShoppingCartId, cancellationToken);
            if (cart == null)
                return Result.Error("Shopping cart not found");

            var order = OrderEntity.Create(
                command.Request.ShoppingCartId,
                command.Request.ShippingAddress,
                command.Request.MobileNumber,
                command.Request.TrackingNumber
            );

            await commandRepository.AddAsync(order, cancellationToken);
            return Result.Success(true);
        }
        catch (ArgumentException ex)
        {
            return Result.Error(ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Error($"Error creating order: {ex.Message}");
        }
    }
}

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Request.ShoppingCartId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);

        RuleFor(x => x.Request.ShippingAddress)
            .MaximumLength(500)
            .WithMessage(SharedValidationMessages.MaxLength);

        RuleFor(x => x.Request.MobileNumber)
            .MaximumLength(20)
            .WithMessage(SharedValidationMessages.MaxLength)
            .Matches(@"^09\d{9}$", RegexOptions.None)
            .WithMessage("Mobile number must be in format 09XXXXXXXXX")
            .When(x => !string.IsNullOrEmpty(x.Request.MobileNumber));
    }
}

public class UpdateOrderStatusCommandHandler(IOrderCommandRepository commandRepository, IOrderQueryRepository queryRepository)
    : ICommandHandler<UpdateOrderStatusCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await queryRepository.GetByIdAsync(command.Request.OrderId, cancellationToken);
            if (order == null)
                return Result.Error("Order not found");

            order.UpdateStatus(command.Request.NewStatus);
            await commandRepository.UpdateAsync(order, cancellationToken);
            return Result.Success(true);
        }
        catch (InvalidOperationException ex)
        {
            return Result.Error(ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Error($"Error updating order status: {ex.Message}");
        }
    }
}

public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusCommandValidator()
    {
        RuleFor(x => x.Request.OrderId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);

        RuleFor(x => x.Request.NewStatus)
            .IsInEnum()
            .WithMessage("Invalid order status");
    }
}

public class UpdateTrackingNumberCommandHandler(IOrderCommandRepository commandRepository, IOrderQueryRepository queryRepository)
    : ICommandHandler<UpdateTrackingNumberCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateTrackingNumberCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await queryRepository.GetByIdAsync(command.Request.OrderId, cancellationToken);
            if (order == null)
                return Result.Error("Order not found");

            order.UpdateTrackingNumber(command.Request.TrackingNumber);
            await commandRepository.UpdateAsync(order, cancellationToken);
            return Result.Success(true);
        }
        catch (InvalidOperationException ex)
        {
            return Result.Error(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return Result.Error(ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Error($"Error updating tracking number: {ex.Message}");
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

public class AddOrderItemCommandHandler(IOrderCommandRepository commandRepository, IOrderQueryRepository queryRepository, IOrderItemRepository itemRepository)
    : ICommandHandler<AddOrderItemCommand, bool>
{
    public async Task<Result<bool>> Handle(AddOrderItemCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await queryRepository.GetByIdAsync(command.OrderId, cancellationToken);
            if (order == null)
                return Result.Error("Order not found");

            var orderItem = OrderItem.Create(
                command.OrderId,
                command.Request.ProductId,
                command.Request.UnitPrice,
                command.Request.Quantity,
                command.Request.DiscountValue
            );

            order.AddOrderItem(orderItem);
            await itemRepository.AddAsync(orderItem, cancellationToken);
            await commandRepository.UpdateAsync(order, cancellationToken);
            return Result.Success(true);
        }
        catch (ArgumentException ex)
        {
            return Result.Error(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Result.Error(ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Error($"Error adding order item: {ex.Message}");
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

public class RemoveOrderItemCommandHandler(IOrderCommandRepository commandRepository, IOrderQueryRepository orderQueryRepository, IOrderItemRepository itemRepository)
    : ICommandHandler<RemoveOrderItemCommand, bool>
{
    public async Task<Result<bool>> Handle(RemoveOrderItemCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var orderItem = await itemRepository.GetByIdAsync(command.OrderItemId, cancellationToken);
            if (orderItem == null)
                return Result.Error("Order item not found");

            var order = await orderQueryRepository.GetByIdAsync(orderItem.OrderId, cancellationToken);
            if (order == null)
                return Result.Error("Order not found");

            order.RemoveOrderItem(orderItem);
            await itemRepository.DeleteAsync(command.OrderItemId, cancellationToken);
            await commandRepository.UpdateAsync(order, cancellationToken);
            return Result.Success(true);
        }
        catch (InvalidOperationException ex)
        {
            return Result.Error(ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Error($"Error removing order item: {ex.Message}");
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

public class UpdateOrderItemCommandHandler(IOrderCommandRepository commandRepository, IOrderQueryRepository orderQueryRepository, IOrderItemRepository itemRepository)
    : ICommandHandler<UpdateOrderItemCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateOrderItemCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await orderQueryRepository.GetByIdAsync(command.OrderId, cancellationToken);
            if (order == null)
                return Result.Error("Order not found");

            var orderItem = await itemRepository.GetByIdAsync(command.Request.Id, cancellationToken);
            if (orderItem == null)
                return Result.Error("Order item not found");

            order.UpdateOrderItem(orderItem, command.Request.Quantity);
            await itemRepository.UpdateAsync(orderItem, cancellationToken);
            await commandRepository.UpdateAsync(order, cancellationToken);
            return Result.Success(true);
        }
        catch (ArgumentException ex)
        {
            return Result.Error(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Result.Error(ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Error($"Error updating order item: {ex.Message}");
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

public class UpdateOrderTotalAmountCommandHandler(IOrderCommandRepository commandRepository, IOrderQueryRepository queryRepository)
    : ICommandHandler<UpdateOrderTotalAmountCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateOrderTotalAmountCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await queryRepository.GetByIdAsync(command.OrderId, cancellationToken);
            if (order == null)
                return Result.Error("Order not found");

            order.UpdateTotalAmount(command.NewTotalAmount);
            await commandRepository.UpdateAsync(order, cancellationToken);
            return Result.Success(true);
        }
        catch (ArgumentException ex)
        {
            return Result.Error(ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Error($"Error updating order total amount: {ex.Message}");
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

        RuleFor(x => x.NewTotalAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage(SharedValidationMessages.GreaterThanZero);
    }
}

