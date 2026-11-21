using Identity.Application.Contract.Interface;
using Identity.Application.Contract.Interface.Addresses;
using Identity.Domain.Entities;
using Identity.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.Repositories.Users;

public sealed class AddressQueryRepository(IdentityWriteDbContext context)
    : QueryRepository<Address>(context), IAddressQueryRepository
{
    public async Task<IReadOnlyCollection<Address>> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        => await Entities
            .AsNoTracking()
            .Where(x => x.UserId == userId && !x.Deleted)
            .OrderByDescending(x => x.IsDefault)
            .ThenByDescending(x => x.Id)
            .ToListAsync(cancellationToken);

    public async Task<Address?> GetByIdAsync(long addressId, long userId, CancellationToken cancellationToken = default)
        => await Entities
            .FirstOrDefaultAsync(x => x.Id == addressId && x.UserId == userId && !x.Deleted, cancellationToken);

    public async Task<Address?> GetDefaultAsync(long userId, CancellationToken cancellationToken = default)
        => await Entities
            .FirstOrDefaultAsync(x => x.UserId == userId && x.IsDefault && !x.Deleted, cancellationToken);
}
