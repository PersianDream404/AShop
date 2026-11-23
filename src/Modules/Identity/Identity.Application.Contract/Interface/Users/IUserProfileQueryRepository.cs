using Identity.Domain.Entities;

namespace Identity.Application.Contract.Interface.Users;

public interface IUserProfileQueryRepository
{
    Task<ApplicationUser?> GetByIdAsync(long userId, CancellationToken cancellationToken = default);
}
