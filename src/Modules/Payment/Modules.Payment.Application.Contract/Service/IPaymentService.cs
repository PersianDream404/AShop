using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Payment.Application.Contract.Service;

using Ardalis.Result;
using Modules.Payment.Application.Contract.DTOs;
using SharedKernel.Interface;

public interface IPaymentService:IScopedDependency
{
    Task<Result<CreatePaymentResultDto>> CreateAsync(
    CreatePaymentRequestDto request,
    CancellationToken ct);

    Task<Result<VerifyPaymentResultDto>> VerifyAsync(
    VerifyPaymentRequestDto request,
    CancellationToken ct);
}
