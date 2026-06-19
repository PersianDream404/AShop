using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Modules.Product.Application.Contract.Interface.Categories;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.FeaturesValuess.Commands;
using Modules.Product.Domain.Interface;
using Modules.Product.Domain.Interface.Categories;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.FeaturesValuess.Commands.Update;

public class ToggleFeaturesValuesCommandHandler(IFeaturesValuesCommandRepository featuresValuesCommandRepository,IFeaturesValuesQueryRepository featuresValuesQueryRepository)
: ICommandHandler<ToggleFeaturesValuesCommand, bool>
{
    public async Task<Result<bool>> Handle(
        ToggleFeaturesValuesCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var featuresValues = await featuresValuesQueryRepository.GetByIdAsync(command.Id, cancellationToken);
            if (featuresValues == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.FeaturesValues));

         
            await featuresValuesCommandRepository.ToggleAsync(featuresValues,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.FeaturesValues));
        }

        return true;
    }
}
public class ToggleFeaturesValuesCommandValidator
    : AbstractValidator<ToggleFeaturesValuesCommand>
{
    public ToggleFeaturesValuesCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required);

    }
}