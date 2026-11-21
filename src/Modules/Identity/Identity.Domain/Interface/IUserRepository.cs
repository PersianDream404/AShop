using Identity.Domain.Entities;
using SharedKernel.Interface;
using SharedKernel.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain.Interface;

public interface IUserQueryRepository 
{
    Task<ApplicationUser> GetByMobile(string mobile);
}


public interface IUserCommandRepository
{
}

public interface IAddressCommandRepository
{
    Task<Address> AddAsync(Address address, CancellationToken cancellationToken = default);
    Task UpdateAsync(Address address, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IAddressQueryRepository
{
    Task<IReadOnlyCollection<Address>> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<Address?> GetByIdAsync(long addressId, long userId, CancellationToken cancellationToken = default);
    Task<Address?> GetDefaultAsync(long userId, CancellationToken cancellationToken = default);
}
