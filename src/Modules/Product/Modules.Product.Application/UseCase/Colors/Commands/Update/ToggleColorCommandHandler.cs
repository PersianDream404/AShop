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

namespace Modules.Product.Application.UseCase.Colors.Commands.Update;

public class ToggleColorCommandHandler(IColorCommandRepository colorCommandRepository,IColorQueryRepository colorQueryRepository)
: ICommandHandler<ToggleColorCommand, bool>
{
    public async Task<Result<bool>> Handle(
        ToggleColorCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var color = await colorQueryRepository.GetByIdAsync(command.Id, cancellationToken);
            if (color == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.Color));

         
            await colorCommandRepository.ToggleAsync(color,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Color));
        }

        return true;
    }
}
public class ToggleColorCommandValidator
    : AbstractValidator<ToggleColorCommand>
{
    public ToggleColorCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required);

    }
}