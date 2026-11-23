using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Identity.Application;
using Identity.Application.Contract.DTOs.Users;
using Identity.Application.Contract.UseCase.Users.Commands;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Users.Commands;

public sealed class UpdateMyProfileCommandHandler(UserManager<ApplicationUser> userManager)
    : ICommandHandler<UpdateMyProfileCommand, UserProfileDto>
{
    public async Task<Result<UserProfileDto>> Handle(
        UpdateMyProfileCommand command,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(command.UserId.ToString());
        if (user is null)
            return Result.NotFound("User not found");

        user.FirstName = command.Request.FirstName.Trim();
        user.LastName = command.Request.LastName.Trim();

        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            return Result.Error(string.Join("; ", updateResult.Errors.Select(error => error.Description)));

        return Result.Success(UserProfileMapper.Map(user));
    }
}

public sealed class UpdateMyProfileCommandValidator : AbstractValidator<UpdateMyProfileCommand>
{
    public UpdateMyProfileCommandValidator()
    {
        RuleFor(command => command.Request.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(command => command.Request.LastName)
            .NotEmpty()
            .MaximumLength(100);
    }
}
