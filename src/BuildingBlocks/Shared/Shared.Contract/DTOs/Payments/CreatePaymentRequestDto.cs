namespace Shared.Contract.DTOs.Payments;

public record CreatePaymentRequestDto(
    int? UserId,
    string? NationalCode,
    long Amount,
    TransactionReason Reason,
    string Description,
    int? CreatorId,
    string? ReturnUrl,
    PaymentCallbackRouteDto CallbackRoute
);
public record VerifyPaymentRequestDto(
    int TransactionId
);

public class ParbadPaymentResultDto
{
    public string GatewayUrl { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
