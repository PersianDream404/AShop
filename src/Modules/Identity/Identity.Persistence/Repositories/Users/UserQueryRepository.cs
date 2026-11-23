using Identity.Domain.Entities;
using Identity.Domain.Interface;
using Identity.Application.Contract.Interface.Users;
using Identity.Persistence.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Interface;

namespace Identity.Persistence.Repositories.Users;

public class UserQueryRepository
    : IUserQueryRepository, IUserProfileQueryRepository
{
    private readonly IdentityWriteDbContext _context;
    public UserQueryRepository(IdentityWriteDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationUser?> GetByMobile(string mobile)
    {
       return await _context.Users
             .FirstOrDefaultAsync(x => x.PhoneNumber == mobile);
    }

    public async Task<ApplicationUser?> GetByIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
    }
}
