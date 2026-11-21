using Identity.Domain.Entities;
using Identity.Domain.Interface;
using Identity.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.Repositories.Users;

public sealed class AddressCommandRepository(IdentityWriteDbContext context) : IAddressCommandRepository
{
    public async Task<Address> AddAsync(Address address, CancellationToken cancellationToken = default)
    {
        await context.Addresses.AddAsync(address, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return address;
    }

    public async Task UpdateAsync(Address address, CancellationToken cancellationToken = default)
    {
        context.Addresses.Update(address);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => await context.SaveChangesAsync(cancellationToken);
}

public sealed class AddressQueryRepository(IdentityWriteDbContext context) : IAddressQueryRepository
{
    public async Task<IReadOnlyCollection<Address>> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        => await context.Addresses
            .AsNoTracking()
            .Where(x => x.UserId == userId && !x.Deleted)
            .OrderByDescending(x => x.IsDefault)
            .ThenByDescending(x => x.Id)
            .ToListAsync(cancellationToken);

    public async Task<Address?> GetByIdAsync(long addressId, long userId, CancellationToken cancellationToken = default)
        => await context.Addresses
            .FirstOrDefaultAsync(x => x.Id == addressId && x.UserId == userId && !x.Deleted, cancellationToken);

    public async Task<Address?> GetDefaultAsync(long userId, CancellationToken cancellationToken = default)
        => await context.Addresses
            .FirstOrDefaultAsync(x => x.UserId == userId && x.IsDefault && !x.Deleted, cancellationToken);
}
