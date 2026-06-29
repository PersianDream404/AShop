using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Payment.Application.Contract.DTOs;

namespace Modules.Orders.Application.DTOs;

public sealed class CreateOrderPaymentRequestDto
{
    public int OrderId { get; init; }

    public string ReturnUrl { get; init; } = default!;
}
namespace Modules.Orders.Application.DTOs;

public sealed class CreateOrderPaymentResultDto
{
    public string GatewayUrl { get; init; } = default!;

    public int OrderTransactionId { get; init; }
}
