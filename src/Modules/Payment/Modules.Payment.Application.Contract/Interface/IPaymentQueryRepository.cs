using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Payment.Application.Contract.Interface;

using Modules.Payment.Domain.Entities;
using SharedKernel.Interface.Repositories;



public interface IPaymentQueryRepository : IQueryRepository<PaymentEntity>
{


}