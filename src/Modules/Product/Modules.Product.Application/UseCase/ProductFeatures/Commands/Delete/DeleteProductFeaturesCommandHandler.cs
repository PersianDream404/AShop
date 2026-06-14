using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.ProductFeaturess.Commands;
using Modules.Product.Domain.Interface;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.ProductFeaturess.Commands.Delete;

public class DeleteProductFeaturesCommandHandler(IProductFeaturesCommandRepository productFeaturesCommandRepository,IProductFeaturesQueryRepository productFeaturesQueryRepository)
: ICommandHandler<DeleteProductFeaturesCommand, bool>
{
    public async Task<Result<bool>> Handle(
        DeleteProductFeaturesCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var productFeatures = await productFeaturesQueryRepository.GetByIdProjectedAsync(command.Id, cancellationToken);
            if (productFeatures == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.ProductFeatures));
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.ProductFeatures));
        }

        return true;
    }
}
public class DeleteProductFeaturesCommandValidator
    : AbstractValidator<DeleteProductFeaturesCommand>
{
    public DeleteProductFeaturesCommandValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ارسال شناسه الزامی است.");
   


    }
}