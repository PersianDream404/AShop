using Framwork.Bus.Query;
using Identity.Application.Contract.DTOs.Users;

namespace Identity.Application.Contract.UseCase.Users.Queries;

public sealed record GetMyProfileQuery(long UserId) : IQuery<UserProfileDto>;
