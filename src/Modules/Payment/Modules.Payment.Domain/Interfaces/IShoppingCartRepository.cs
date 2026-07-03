using Modules.Payment.Domain.Entities;
using SharedKernel.Interface.Repositories;

namespace Modules.Order.Domain.Interfaces;

public interface IPaymentCommandRepository : ICommandRepository<PaymentEntity>
{

}
