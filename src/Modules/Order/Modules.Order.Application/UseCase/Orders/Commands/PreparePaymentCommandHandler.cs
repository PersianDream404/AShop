using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Mapster;
using MediatR;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.Interface.ShoppingCarts;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using Modules.Order.Domain.Entities;
using Modules.Order.Domain.Enums;
using Modules.Order.Domain.Interfaces;
using Modules.Payment.Application.Contract.DTOs;
using Modules.Payment.Application.Contract.Service;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface.Repositories;
using System.Text.RegularExpressions;

namespace Modules.Order.Application.UseCase.Orders.Commands;

public class PreparePaymentCommandHandler(
    IOrderCommandRepository commandRepository,
    IPaymentService paymentService,
    IShoppingCartQueryRepository cartQueryRepository,
    IOrderQueryRepository orderQueryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<PreparePaymentCommand, GetPreparePaymentResponseDto>
{
    public async Task<Result<GetPreparePaymentResponseDto>> Handle(PreparePaymentCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var order = await orderQueryRepository.GetByIdAsync(command.OrderId, cancellationToken);

            if (order is null)
                return Result.NotFound("Order not found.");

            var transaction = order.CreatePaymentTransaction();
            await commandRepository.AttachAsync(order, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var paymentResult = await paymentService.CreateAsync(
                new CreatePaymentRequestDto
                {
                    Amount = transaction.Amount,
                    TrackingNumber = transaction.Id,
                    SuccessReturnUrl = command.request.SuccessReturnUrl,
                    FailedReturnUrl = command.request.FailedReturnUrl
                },
                cancellationToken);

            if (!paymentResult.IsSuccess)
                return Result.Error(paymentResult.Errors.First());

            return Result.Success(new GetPreparePaymentResponseDto
            {
                GatewayUrl = paymentResult.Value.GatewayUrl,
                OrderTransactionId = transaction.Id
            });
        }
        catch (Exception ex)
        {
            return Result.Error($"{OrderValidationMessages.ErrorCreatingOrder}: {ex.Message}");
        }
    }
}

public class PreparePaymentCommandValidator : AbstractValidator<PreparePaymentCommand>
{
    public PreparePaymentCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);
    }
}