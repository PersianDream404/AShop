using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Mapster;
using Modules.Product.Application.Contract.UseCase.Products.Commands;
using Modules.Product.Domain.Entities.Products;
using Modules.Product.Domain.Interface.Products;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Identity.Application.Products.Queries.Create;

public class CreateProductCommandHandler(IProductCommandRepository ProductCommandRepository)
: ICommandHandler<CreateProductCommand, bool>
{
    public async Task<Result<bool>> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var product = command.request.Adapt<Product>();
            await ProductCommandRepository.AddAsync(product);

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
            .NotEmpty().WithMessage("عنوان محصول الزامی است")
            .MaximumLength(300).WithMessage("عنوان محصول نمی‌تواند بیشتر از 300 کاراکتر باشد");

        RuleFor(x => x.request.Code)
            .MaximumLength(300).WithMessage("کد محصول نمی‌تواند بیشتر از 300 کاراکتر باشد");

        RuleFor(x => x.request.Price)
            .GreaterThan(0).WithMessage("قیمت محصول باید بیشتر از صفر باشد");

        RuleFor(x => x.request.ShortDescription)
            .MaximumLength(300).WithMessage("توضیحات کوتاه نمی‌تواند بیشتر از 300 کاراکتر باشد");

        RuleFor(x => x.request.Description)
            .MaximumLength(5000).WithMessage("توضیحات کامل بیش از حد مجاز است");

        RuleFor(x => x.request.Image)
            .MaximumLength(500).WithMessage("مسیر تصویر معتبر نیست");

        RuleFor(x => x.request.IsActive)
            .NotNull().WithMessage("وضعیت فعال/غیرفعال مشخص نشده است");

        // =====================
        // دسته‌بندی‌ها
        // =====================
        RuleFor(x => x.request.CategoriesIds)
            .NotNull().WithMessage("انتخاب دسته‌بندی الزامی است")
            .Must(x => x.Any()).WithMessage("حداقل یک دسته‌بندی باید انتخاب شود");

        RuleForEach(x => x.request.CategoriesIds)
            .GreaterThan(0).WithMessage("شناسه دسته‌بندی نامعتبر است");

        // =====================
        // رنگ‌ها
        // =====================
        RuleForEach(x => x.request.SelectedColorsIds)
            .GreaterThan(0).WithMessage("شناسه رنگ نامعتبر است");

        // =====================
        // تخفیف‌ها
        // =====================
        RuleForEach(x => x.request.DiscountsIds)
            .GreaterThan(0).WithMessage("شناسه تخفیف نامعتبر است");

        // =====================
        // گالری تصاویر
        // =====================
        RuleFor(x => x.request.ProductGalleries)
            .Must(x => x.Count <= 10)
            .WithMessage("حداکثر 10 تصویر برای گالری مجاز است");

        RuleForEach(x => x.request.ProductGalleries)
            .ChildRules(gallery =>
            {
                gallery.RuleFor(x => x.ImageName)
                    .NotEmpty().WithMessage("تصویر گالری الزامی است")
                    .MaximumLength(500).WithMessage("نام تصویر بیش از حد مجاز است");

                gallery.RuleFor(x => x.DisplayPriority)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("اولویت نمایش نمی‌تواند منفی باشد");
            });

        // =====================
        // ویژگی‌ها
        // =====================
        RuleForEach(x => x.request.ProductFeatures)
            .ChildRules(feature =>
            {
                feature.RuleFor(x => x.FeatureValue)
                    .NotEmpty().WithMessage("مقدار ویژگی الزامی است")
                    .MaximumLength(300).WithMessage("مقدار ویژگی بیش از حد مجاز است");

                feature.RuleFor(x => x.ProductFeaturesCategoryId)
                    .GreaterThan(0)
                    .When(x => x.ProductFeaturesCategoryId.HasValue)
                    .WithMessage("دسته‌بندی ویژگی نامعتبر است");

                feature.RuleFor(x => x.ProductFeaturesId)
                    .GreaterThan(0)
                    .When(x => x.ProductFeaturesId.HasValue)
                    .WithMessage("شناسه ویژگی نامعتبر است");
            });
    }
}