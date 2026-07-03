using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Payment.Application.Contract.Events;

using SharedKernel.Events;


public sealed record PaymentSucceededEvent(
long PaymentId,
long TrackingNumber,
decimal Amount,
string GatewayTransactionCode
) : IEvent;


public sealed record PaymentFailedEvent(
long PaymentId,
long TrackingNumber,
decimal Amount
) : IEvent;
