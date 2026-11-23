using Ardalis.Result;
using Framwork.Bus.Query;
using Identity.Application;
using Identity.Application.Contract.DTOs.Users;
using Identity.Application.Contract.Interface.Users;
using Identity.Application.Contract.UseCase.Users.Queries;

namespace Identity.Application.Users.Queries;

public sealed class GetMyProfileQueryHandler(IUserProfileQueryRepository queryRepository)
    : IQueryHandler<GetMyProfileQuery, UserProfileDto>
{
    public async Task<Result<UserProfileDto>> Handle(
        GetMyProfileQuery query,
        CancellationToken cancellationToken)
    {
        var user = await queryRepository.GetByIdAsync(query.UserId, cancellationToken);
        return user is null
            ? Result.NotFound("User not found")
            : Result.Success(UserProfileMapper.Map(user));
    }
}
