using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Mapster;
using Modules.Product.Application.Contract.Interface.Categories;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.Categorys.Commands;
using Modules.Product.Domain.Interface;
using Modules.Product.Domain.Interface.Categories;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Categorys.Commands.Update;

public class UpdateCategoryCommandHandler(ICategoryCommandRepository categoryCommandRepository,ICategoryQueryRepository categoryQueryRepository)
: ICommandHandler<UpdateCategoryCommand, bool>
{
    public async Task<Result<bool>> Handle(
        UpdateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var category = await categoryQueryRepository.GetByIdAsync(command.request.Id, cancellationToken);
            if (category == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.Category));

            if (!await categoryQueryRepository.IsUniqueAsync(x => x.Title == command.request.Title, false, cancellationToken))
                return Result.Error(MessageHelper.Format(AppMessages.Found, AppEntityCategory.Title));
            command.request.Adapt(category);
            await categoryCommandRepository.UpdateAsync(category,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Category));
        }

        return true;
    }
}
public class UpdateCategoryCommandValidator
    : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {

        RuleFor(x => x.request.Id)
            .NotEmpty()
            .WithMessage("ارسال شناسه الزامی است.");

        RuleFor(x => x.request.Title)
            .NotEmpty()
            .WithMessage("نام دسته‌بندی الزامی است.")
            .MaximumLength(250)
            .WithMessage("نام دسته‌بندی نمی‌تواند بیشتر از ۲۵۰ کاراکتر باشد.");

        RuleFor(x => x.request.UrlName)
            .NotEmpty()
            .WithMessage("نام URL الزامی است.")
            .MaximumLength(250)
            .WithMessage("نام URL نمی‌تواند بیشتر از ۲۵۰ کاراکتر باشد.")
            .Matches("^[a-z0-9-]+$")
            .WithMessage("نام URL فقط می‌تواند شامل حروف انگلیسی کوچک، عدد و - باشد.");

        RuleFor(x => x.request.Image)
            .MaximumLength(250)
            .WithMessage("مسیر تصویر نمی‌تواند بیشتر از ۲۵۰ کاراکتر باشد.")
            .When(x => !string.IsNullOrWhiteSpace(x.request.Image));

        RuleFor(x => x.request.Icon)
            .MaximumLength(250)
            .WithMessage("آیکن نمی‌تواند بیشتر از ۲۵۰ کاراکتر باشد.")
            .When(x => !string.IsNullOrWhiteSpace(x.request.Icon));
    }
}