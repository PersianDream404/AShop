using Identity.Domain.Entities;
using Identity.Domain.Interface;
using Identity.Persistence.Context;
using Identity.Persistence.Repositories;

namespace Identity.Persistence.Repositories.Users;

public sealed class AddressCommandRepository(IdentityWriteDbContext context)
    : CommandRepository<Address>(context), IAddressCommandRepository
{
}
