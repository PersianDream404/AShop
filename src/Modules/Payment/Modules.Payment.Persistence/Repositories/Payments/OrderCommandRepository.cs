using Infrastructure.Repositories;
using Modules.Order.Domain.Interfaces;
using Modules.Payment.Domain.Entities;
using Modules.Payment.Persistence.Context;

namespace Modules.Payment.Persistence.Repositories.Payments;

public class PaymentCommandRepository : CommandRepository<PaymentEntity>, IPaymentCommandRepository
{
    private readonly PaymentWriteDbContext _context;

    public PaymentCommandRepository(PaymentWriteDbContext context) : base(context)
    {
        _context = context;
    }
   

}
