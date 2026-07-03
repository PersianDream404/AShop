namespace Modules.Payment.Application.Contract.DTOs;

public sealed class CreatePaymentResultDto
{
    public long PaymentId { get; init; }

    public string GatewayUrl { get; init; } = default!;
}
