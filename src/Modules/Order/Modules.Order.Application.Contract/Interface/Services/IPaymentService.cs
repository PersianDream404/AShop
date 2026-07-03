using Ardalis.Result;
using Shared.Contract.DTOs.Payments;
using SharedKernel.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Order.Application.Contract.Interface.Services;

public interface IPaymentService : IScopedDependency
{
    Task<Result<ParbadPaymentResultDto>> CreatePaymentAsync(
        CreatePaymentRequestDto request,
        CancellationToken ct);

    Task<Result> VerifyPaymentAsync(
        VerifyPaymentRequestDto request,
        CancellationToken ct);
}