using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.ProductFeaturess.Commands;
using Modules.Product.Domain.Interface;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.ProductFeaturess.Commands.Update;

public class ToggleProductFeaturesCommandHandler(IProductFeaturesCommandRepository productFeaturesCommandRepository,IProductFeaturesQueryRepository productFeaturesQueryRepository)
: ICommandHandler<ToggleProductFeaturesCommand, bool>
{
    public async Task<Result<bool>> Handle(
        ToggleProductFeaturesCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var productFeatures = await productFeaturesQueryRepository.GetByIdAsync(command.Id, cancellationToken);
            if (productFeatures == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.ProductFeatures));

         
            await productFeaturesCommandRepository.ToggleAsync(productFeatures,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.ProductFeatures));
        }

        return true;
    }
}
public class ToggleProductFeaturesCommandValidator
    : AbstractValidator<ToggleProductFeaturesCommand>
{
    public ToggleProductFeaturesCommandValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required);

    }
}