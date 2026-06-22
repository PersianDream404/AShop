using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Modules.Product.Application.Contract.Interface.Categories;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.FeaturesValuess.Commands;
using Modules.Product.Domain.Interface;
using Modules.Product.Domain.Interface.Categories;
using Modules.Product.Domain.Interface.FeaturesCategories;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.FeaturesValuess.Commands.Delete;

public class DeleteFeaturesValuesCommandHandler(IFeaturesValuesCommandRepository featuresValuesCommandRepository,IFeaturesValuesQueryRepository featuresValuesQueryRepository)
: ICommandHandler<DeleteFeaturesValuesCommand, bool>
{
    public async Task<Result<bool>> Handle(
        DeleteFeaturesValuesCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var featuresValues = await featuresValuesQueryRepository.GetByIdProjectedAsync(command.Id, cancellationToken);
            if (featuresValues == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.FeaturesValues));

            await featuresValuesCommandRepository.DeleteAsync(command.Id);
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.FeaturesValues));
        }

        return true;
    }
}
public class DeleteFeaturesValuesCommandValidator
    : AbstractValidator<DeleteFeaturesValuesCommand>
{
    public DeleteFeaturesValuesCommandValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required);



    }
}