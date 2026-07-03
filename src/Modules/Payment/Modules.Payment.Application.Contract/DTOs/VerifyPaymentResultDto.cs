namespace Modules.Payment.Application.Contract.DTOs;

public sealed class VerifyPaymentResultDto
{
    public string ReturnUrl { get; init; } = default!;

    public bool IsSucceeded { get; init; }
}
