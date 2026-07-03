namespace Modules.Payment.Application.Services;

using Ardalis.Result;
using global::Modules.Order.Domain.Interfaces;
using global::Modules.Payment.Application.Contract.DTOs;
using global::Modules.Payment.Application.Contract.Interface;
using global::Modules.Payment.Application.Contract.Service;
using Modules.Payment.Application.Contract.Events;
using Modules.Payment.Domain.Entities;
using Modules.Payment.Domain.Enums;
using Parbad;
using Parbad.Gateway.ZarinPal;
using SharedKernel.Events;
using SharedKernel.Interface.Repositories;

public sealed class PaymentService(
    IOnlinePayment onlinePayment,
    IPaymentCommandRepository paymentCommandRepository,
    IPaymentQueryRepository paymentQueryRepository,
    IPaymentCallbackUrlFactory callbackUrlFactory,
    IUnitOfWork unitOfWork,
    IEventBus eventBus
) : IPaymentService
{
    public async Task<Result<CreatePaymentResultDto>> CreateAsync(
        CreatePaymentRequestDto request,
        CancellationToken ct)
    {
        if (request.Amount <= 0)
        {
            return Result.Invalid(new ValidationError
            {
                Identifier = "Payment.Amount.Invalid",
                ErrorMessage = "Amount must be greater than zero."
            });
        }

        var payment = new PaymentEntity(
            request.Amount,
            request.TrackingNumber,
            request.SuccessReturnUrl,request.FailedReturnUrl);

        // استفاده صحیح از Repository تعیین شده در Constructor
        await paymentCommandRepository.AddAsync(payment, ct);
        await unitOfWork.SaveChangesAsync(ct);

        var callbackUrl = callbackUrlFactory.Create(payment.Id);

        var gatewayResult = await onlinePayment.RequestAsync(invoice =>
        {
            invoice
                .UseZarinPal()
                .SetZarinPalData("پرداخت سفارش", "support@site.com")
                .SetAmount(payment.Amount)
                .SetCallbackUrl(callbackUrl);

            invoice.SetTrackingNumber(payment.Id);
        });

        if (!gatewayResult.IsSucceed)
        {
            payment.MarkFailed();
            await unitOfWork.SaveChangesAsync(ct);

            return Result.Error("Failed to create payment request.");
        }

        return Result.Success(new CreatePaymentResultDto
        {
            PaymentId = payment.Id,
            GatewayUrl = gatewayResult.GatewayTransporter.Descriptor.Url
        });
    }

    public async Task<Result<VerifyPaymentResultDto>> VerifyAsync(
        VerifyPaymentRequestDto request,
        CancellationToken ct)
    {
        var payment = await paymentQueryRepository.GetByIdAsync(request.PaymentId, ct);

        if (payment is null)
            return Result.NotFound("Payment not found.");

        if (payment.PaymentStatus == PaymentStatus.Succeeded)
        {
            return Result.Success(new VerifyPaymentResultDto
            {
                ReturnUrl = BuildReturnUrl(payment.SuccessReturnUrl!, true),
                IsSucceeded = true
            });
        }

        IPaymentFetchResult invoice;

        try
        {
            invoice = await onlinePayment.FetchAsync();
        }
        catch
        {
            payment.MarkFailed();
            await paymentCommandRepository.UpdateAsync(payment, ct);
            await unitOfWork.SaveChangesAsync(ct);

            // اصلاح کد ناقص بلاک Catch جهت انتشار صحیح رویداد شکست پرداخت
            await eventBus.PublishAsync(
                new PaymentFailedEvent(payment.Id, payment.TrackingNumber, payment.Amount),
                ct);

            return Result.Success(new VerifyPaymentResultDto
            {
                ReturnUrl = BuildReturnUrl(payment.FailedReturnUrl!, false),
                IsSucceeded = false
            });
        }

        if (!invoice.IsSucceed)
        {
            payment.MarkFailed();
            await paymentCommandRepository.UpdateAsync(payment, ct);
            await unitOfWork.SaveChangesAsync(ct);

            await eventBus.PublishAsync(
                new PaymentFailedEvent(payment.Id, payment.TrackingNumber, payment.Amount),
                ct);

            return Result.Success(new VerifyPaymentResultDto
            {
                ReturnUrl = BuildReturnUrl(payment.FailedReturnUrl, false),
                IsSucceeded = false
            });
        }

        var verifyResult = await onlinePayment.VerifyAsync(invoice);

        if (!verifyResult.IsSucceed)
        {
            payment.MarkFailed();
            await paymentCommandRepository.UpdateAsync(payment, ct);
            await unitOfWork.SaveChangesAsync(ct);

            await eventBus.PublishAsync(
                new PaymentFailedEvent(payment.Id, payment.TrackingNumber, payment.Amount),
                ct);

            return Result.Success(new VerifyPaymentResultDto
            {
                ReturnUrl = BuildReturnUrl(payment.FailedReturnUrl, false),
                IsSucceeded = false
            });
        }

        payment.MarkSucceeded(verifyResult.TransactionCode!);

        await paymentCommandRepository.UpdateAsync(payment, ct);
        await unitOfWork.SaveChangesAsync(ct);

        await eventBus.PublishAsync(
            new PaymentSucceededEvent(
                payment.Id,
                payment.TrackingNumber,
                payment.Amount,
                verifyResult.TransactionCode!),
            ct);

        return Result.Success(new VerifyPaymentResultDto
        {
            ReturnUrl = BuildReturnUrl(payment.SuccessReturnUrl, true),
            IsSucceeded = true
        });
    }

    private static string BuildReturnUrl(string returnUrl, bool isSucceeded)
    {
        var separator = returnUrl.Contains('?') ? "&" : "?";
        var status = isSucceeded ? "success" : "failed";

        return $"{returnUrl}{separator}paymentStatus={status}";
    }
}
