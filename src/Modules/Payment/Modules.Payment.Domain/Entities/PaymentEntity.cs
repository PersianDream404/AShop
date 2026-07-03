using Modules.Payment.Domain.Enums;
using SharedKernel.Base;

namespace Modules.Payment.Domain.Entities;

public class PaymentEntity:BaseEntityIdentity
{

    public decimal Amount { get; private set; }

    public long TrackingNumber { get; private set; }

    public PaymentStatus PaymentStatus { get; private set; }

    public string? TransactionCode { get; private set; }
    public string? SuccessReturnUrl { get; private set; }
    public string? FailedReturnUrl { get; private set; }

    public PaymentEntity(decimal amount, long trackingNumber,string? successReturnUrl,string? failedReturnUrl)
    {
        Amount = amount;
        TrackingNumber = trackingNumber;
        PaymentStatus = PaymentStatus.Pending;
        SuccessReturnUrl = successReturnUrl;
        FailedReturnUrl = failedReturnUrl;
    }

    public void MarkSucceeded(string code)
    {
        PaymentStatus = PaymentStatus.Succeeded;
        TransactionCode = code;
    }

    public void MarkFailed()
    {
        PaymentStatus = PaymentStatus.Failed;
    }
}

