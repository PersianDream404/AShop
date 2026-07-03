using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Payment.Domain.Enums;

public enum PaymentStatus
{
    Pending = 1,
    Succeeded = 2,
    Failed = 3
}
