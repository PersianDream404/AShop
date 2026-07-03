using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Payment.Application.Contract.DTOs;



public sealed class CreatePaymentRequestDto
{
    public decimal Amount { get; init; }

    public long TrackingNumber { get; init; }

    public string? SuccessReturnUrl { get; init; } 
    public string? FailedReturnUrl { get; init; } 
}
