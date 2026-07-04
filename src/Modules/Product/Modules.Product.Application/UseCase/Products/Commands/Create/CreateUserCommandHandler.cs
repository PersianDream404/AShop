using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Product.Application.Contract.UseCase.Products.Commands;
using Modules.Product.Domain.Entities.Products;
using Modules.Product.Domain.Interface.Products;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Products.Commands.Create;

public class CreateProductCommandHandler(IProductCommandRepository productCommandRepository)
: ICommandHandler<CreateProductCommand, bool>
{
    public async Task<Result<bool>> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
          
            var product = command.request.Adapt<Modules.Product.Domain.Entities.Products.Product>();
            await productCommandRepository.AddAsync(product);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Product));
        }

        return true;
    }
}
public class CreateProductCommandValidator
    : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        // =====================
        // اطلاعات اصلی محصول
        // =====================
        RuleFor(x => x.request.Title)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required)
            .MaximumLength(300)
            .WithMessage(SharedValidationMessages.MaxLength);

        RuleFor(x => x.request.Code)
            .MaximumLength(300)
            .WithMessage(SharedValidationMessages.MaxLength);

        RuleFor(x => x.request.Price)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.GreaterThanZero);

        RuleFor(x => x.request.ShortDescription)
            .MaximumLength(300)
            .WithMessage(SharedValidationMessages.MaxLength);

        RuleFor(x => x.request.Description)
            .MaximumLength(5000)
            .WithMessage(SharedValidationMessages.MaxLength);

        RuleFor(x => x.request.Image)
            .MaximumLength(500)
            .WithMessage(SharedValidationMessages.MaxLength);

        //RuleFor(x => x.request.IsActive)
        //    .NotNull()
        //    .WithMessage(SharedValidationMessages.Required);

        // =====================
        // دسته‌بندی‌ها
        // =====================
        RuleFor(x => x.request.CategoriesIds)
            .NotNull()
            .WithMessage(SharedValidationMessages.Required)
            .Must(x => x.Any())
            .WithMessage(SharedValidationMessages.AtLeastOneRequired);

        RuleForEach(x => x.request.CategoriesIds)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.Invalid);

        // =====================
        // رنگ‌ها
        // =====================
        RuleForEach(x => x.request.ColorsIds)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.Invalid);

        // =====================
        // تخفیف‌ها
        // =====================
        //RuleForEach(x => x.request.DiscountsIds)
        //    .GreaterThan(0)
        //    .WithMessage(SharedValidationMessages.InvalidId);

        // =====================
        // گالری تصاویر
        // =====================
        RuleFor(x => x.request.ProductGalleries)
            .Must(x => x.Count <= 10)
            .WithMessage(SharedValidationMessages.MaxItems);

        RuleForEach(x => x.request.ProductGalleries)
            .ChildRules(gallery =>
            {
                gallery.RuleFor(x => x.ImageName)
                    .NotEmpty()
                    .WithMessage(SharedValidationMessages.Required)
                    .MaximumLength(500)
                    .WithMessage(SharedValidationMessages.MaxLength);

                gallery.RuleFor(x => x.DisplayPriority)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage(SharedValidationMessages.InvalidNumber);
            });

        // =====================
        // ویژگی‌ها
        // =====================
        RuleForEach(x => x.request.ProductFeatures)
            .ChildRules(feature =>
            {
                feature.RuleFor(x => x.FeatureValue)
                    .NotEmpty()
                    .WithMessage(SharedValidationMessages.Required)
                    .MaximumLength(300)
                    .WithMessage(SharedValidationMessages.MaxLength);

                feature.RuleFor(x => x.ProductFeaturesCategoryId)
                    .GreaterThan(0)
                    .When(x => x.ProductFeaturesCategoryId.HasValue)
                    .WithMessage(SharedValidationMessages.Invalid);

                feature.RuleFor(x => x.ProductFeaturesId)
                    .GreaterThan(0)
                    .When(x => x.ProductFeaturesId.HasValue)
                    .WithMessage(SharedValidationMessages.Invalid);
            });

    }
}