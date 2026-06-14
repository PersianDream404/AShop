using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Mapster;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.ProductFeaturess.Commands;
using Modules.Product.Domain.Entities.Features;
using Modules.Product.Domain.Interface;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.ProductFeaturess.Commands.Create;

public class CreateProductFeaturesCommandHandler(IProductFeaturesCommandRepository ProductFeaturesCommandRepository, IProductFeaturesQueryRepository productFeaturesQueryRepository)
: ICommandHandler<CreateProductFeaturesCommand, bool>
{
    public async Task<Result<bool>> Handle(
        CreateProductFeaturesCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!await productFeaturesQueryRepository.IsUniqueAsync(x => x.Title == command.request.Title, true,cancellationToken))
                return Result.Error(MessageHelper.Format(AppMessages.Found, AppEntityProductFeatures.Title));
            var productFeatures = command.request.Adapt<ProductFeatures>();
            await ProductFeaturesCommandRepository.AddAsync(productFeatures,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.ProductFeatures));
        }

        return true;
    }
}


public class CreateProductFeaturesCommandValidator : AbstractValidator<CreateProductFeaturesCommand>
{
    public CreateProductFeaturesCommandValidator()
    {
       

        RuleFor(x => x.request.Title)
            .NotEmpty()
            .WithMessage("  نام  الزامی است.")
            .MaximumLength(250)
            .WithMessage(" نام نمی‌تواند بیشتر از ۲۵۰ کاراکتر باشد.");
    }
}