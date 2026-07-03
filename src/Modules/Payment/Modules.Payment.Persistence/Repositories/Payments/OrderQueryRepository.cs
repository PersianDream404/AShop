using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Payment.Application.Contract.DTOs;
using Modules.Payment.Domain.Entities;
using Modules.Payment.Persistence.Mapper.Payments;
using Modules.Payment.Persistence.Context;
using Modules.Payment.Application.Contract.Interface;

namespace Modules.Payment.Persistence.Repositories.Payments;

public class PaymentQueryRepository : QueryRepository<PaymentEntity>, IPaymentQueryRepository
{
    private readonly PaymentReadDbContext _context;

    public PaymentQueryRepository(PaymentReadDbContext context) : base(context)
    {
        _context = context;
    }



}
