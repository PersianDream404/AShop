using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Payment.Application.Contract.Service;

    using Ardalis.Result;
    using global::Modules.Payment.Application.Contract.DTOs.Modules.Orders.Application.DTOs;
    using global::Modules.Payment.Application.Contract.DTOs.Modules.Orders.Application.DTOs.Modules.Orders.Application.DTOs;


    public interface IOrderPaymentService
    {
        Task<Result<CreateOrderPaymentResultDto>> CreatePaymentAsync(
        CreateOrderPaymentRequestDto request,
        CancellationToken ct);
    }


