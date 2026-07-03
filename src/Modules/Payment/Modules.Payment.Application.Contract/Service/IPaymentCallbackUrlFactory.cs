using SharedKernel.Interface;

namespace Modules.Payment.Application.Contract.Service;

public interface IPaymentCallbackUrlFactory:IScopedDependency
{
    string Create(long paymentId);
}