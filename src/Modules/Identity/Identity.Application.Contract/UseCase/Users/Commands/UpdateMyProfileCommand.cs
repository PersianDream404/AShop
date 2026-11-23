using Framwork.Bus.Command;
using Identity.Application.Contract.DTOs.Users;

namespace Identity.Application.Contract.UseCase.Users.Commands;

public sealed record UpdateMyProfileCommand(long UserId, UpdateMyProfileRequestDto Request) : ICommand<UserProfileDto>;
