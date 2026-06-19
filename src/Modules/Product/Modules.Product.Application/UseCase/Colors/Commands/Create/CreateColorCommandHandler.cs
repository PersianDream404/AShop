using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Product.Application.Contract.Interface.Colors;
using Modules.Product.Application.Contract.UseCase.Colors.Commands;
using Modules.Product.Domain.Entities.Colors;
using Modules.Product.Domain.Interface.Colors;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Colors.Commands.Create;

public class CreateColorCommandHandler(IColorCommandRepository ColorCommandRepository, IColorQueryRepository colorQueryRepository)
: ICommandHandler<CreateColorCommand, bool>
{
    public async Task<Result<bool>> Handle(
        CreateColorCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!await colorQueryRepository.IsUniqueAsync(x => x.ColorCode == command.request.ColorCode, true,cancellationToken))
                return Result.Error(MessageHelper.Format(AppMessages.Found, AppEntityColor.ColorCode));
            var color = command.request.Adapt<Color>();
            await ColorCommandRepository.AddAsync(color,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Color));
        }

        return true;
    }
}


public class CreateColorCommandValidator : AbstractValidator<CreateColorCommand>
{
    public CreateColorCommandValidator()
    {
        RuleFor(x => x.request.ColorCode)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required)
            .MaximumLength(250)
            .WithMessage(SharedValidationMessages.MaxLength);

        RuleFor(x => x.request.ColorName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required)
            .MaximumLength(250)
            .WithMessage(SharedValidationMessages.MaxLength);

    }
}