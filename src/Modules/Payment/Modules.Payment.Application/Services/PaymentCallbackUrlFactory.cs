using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Payment.Application.Services;

using global::Modules.Payment.Application.Contract.Service;
using Microsoft.AspNetCore.Http;


public sealed class PaymentCallbackUrlFactory(
    IHttpContextAccessor httpContextAccessor
) : IPaymentCallbackUrlFactory
{
    public string Create(long paymentId)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is null)
            throw new InvalidOperationException("HttpContext is not available.");

        var request = httpContext.Request;

        return $"{request.Scheme}://{request.Host}/api/payment/{paymentId}/callback";
    }
}
