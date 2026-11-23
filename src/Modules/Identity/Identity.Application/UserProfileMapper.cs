using Identity.Application.Contract.DTOs.Users;
using Identity.Domain.Entities;

namespace Identity.Application;

internal static class UserProfileMapper
{
    public static UserProfileDto Map(ApplicationUser user) => new()
    {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Mobile = user.PhoneNumber
    };
}
