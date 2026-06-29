namespace FAPN.Application.Services.Payments;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Contract.DTOs.Payments;

public interface IPaymentCallbackUrlFactory
{
    string CreateUrl(PaymentCallbackRouteDto callbackRoute);
}



