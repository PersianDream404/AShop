namespace FAPN.Application.Services.Payments;

using Ardalis.Result;
using Shared.Contract.DTOs.Payments;
using SharedKernel.Interface;

public interface IPaymentService : IScopedDependency
{
    Task<Result<ParbadPaymentResultDto>> CreatePaymentAsync(
        CreatePaymentRequestDto request,
        CancellationToken ct);

    Task<Result> VerifyPaymentAsync(
        VerifyPaymentRequestDto request,
        CancellationToken ct);
}