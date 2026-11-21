using Identity.Domain.Entities;

namespace Identity.Application.Contract.Interface.Addresses;

public interface IAddressQueryRepository : IQueryRepository<Address>
{
    Task<IReadOnlyCollection<Address>> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<Address?> GetByIdAsync(long addressId, long userId, CancellationToken cancellationToken = default);
    Task<Address?> GetDefaultAsync(long userId, CancellationToken cancellationToken = default);
}
