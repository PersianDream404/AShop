using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Mapster;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.ProductFeaturess.Commands;
using Modules.Product.Domain.Interface;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.ProductFeaturess.Commands.Update;

public class UpdateProductFeaturesCommandHandler(IProductFeaturesCommandRepository productFeaturesCommandRepository,IProductFeaturesQueryRepository productFeaturesQueryRepository)
: ICommandHandler<UpdateProductFeaturesCommand, bool>
{
    public async Task<Result<bool>> Handle(
        UpdateProductFeaturesCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var productFeatures = await productFeaturesQueryRepository.GetByIdAsync(command.request.Id, cancellationToken);
            if (productFeatures == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.ProductFeatures));

            if (!await productFeaturesQueryRepository.IsUniqueAsync(x => x.Title == command.request.Title, false, cancellationToken))
                return Result.Error(MessageHelper.Format(AppMessages.Found, AppEntityProductFeatures.Title));
            command.request.Adapt(productFeatures);
            await productFeaturesCommandRepository.UpdateAsync(productFeatures,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.ProductFeatures));
        }

        return true;
    }
}
public class UpdateProductFeaturesCommandValidator
    : AbstractValidator<UpdateProductFeaturesCommand>
{
    public UpdateProductFeaturesCommandValidator()
    {

        RuleFor(x => x.request.Id)
            .NotEmpty()
            .WithMessage("ارسال شناسه الزامی است.");


        RuleFor(x => x.request.Title)
            .NotEmpty()
            .WithMessage("  نام  الزامی است.")
            .MaximumLength(250)
            .WithMessage(" نام نمی‌تواند بیشتر از ۲۵۰ کاراکتر باشد.");
    }
}