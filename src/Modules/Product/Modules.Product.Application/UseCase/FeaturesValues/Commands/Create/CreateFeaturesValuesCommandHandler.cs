using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Mapster;
using Modules.Product.Application.Contract.Interface.Categories;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.FeaturesValuess.Commands;
using Modules.Product.Domain.Entities.Categories;
using Modules.Product.Domain.Entities.Features;
using Modules.Product.Domain.Interface;
using Modules.Product.Domain.Interface.Categories;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.FeaturesValuess.Commands.Create;

public class CreateFeaturesValuesCommandHandler(IFeaturesValuesCommandRepository FeaturesValuesCommandRepository, IFeaturesValuesQueryRepository featuresValuesQueryRepository)
: ICommandHandler<CreateFeaturesValuesCommand, bool>
{
    public async Task<Result<bool>> Handle(
        CreateFeaturesValuesCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            //if (!await featuresValuesQueryRepository.IsUniqueAsync(x => x.FeatureValue == command.request.Title, true,cancellationToken))
            //    return Result.Error(MessageHelper.Format(AppMessages.Found, AppEntityFeaturesValues.Title));
            var featuresValues = command.request.Adapt<FeaturesValues>();
            await FeaturesValuesCommandRepository.AddAsync(featuresValues,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.FeaturesValues));
        }

        return true;
    }
}


public class CreateFeaturesValuesCommandValidator : AbstractValidator<CreateFeaturesValuesCommand>
{
    public CreateFeaturesValuesCommandValidator()
    {
        RuleFor(x => x.request.FeatureValue)
            .NotEmpty()
            .WithMessage("مقدار قابلیت الزامی است.")
            .MaximumLength(250)
            .WithMessage("مقدار قابلیت ‌ نمی‌تواند بیشتر از ۲۵۰ کاراکتر باشد.");

        RuleFor(x => x.request.ProductFeaturesCategoryId)
            .NotEmpty()
            .WithMessage("ارسال شناسه  دسته بندی قابلیت الزامی است.");

        RuleFor(x => x.request.ProductFeaturesId)
            .NotEmpty()
            .WithMessage("ارسال شناسه  قابلیت الزامی است.");
    }
}
