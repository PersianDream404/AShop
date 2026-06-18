using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Decorator.Query;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Product.Application.Contract.DTOs.Categorys.Create;
using Modules.Product.Application.Contract.Interface.Categories;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.Categorys.Commands;
using Modules.Product.Domain.Entities.Categories;
using Modules.Product.Domain.Entities.Features;
using Modules.Product.Domain.Interface;
using Modules.Product.Domain.Interface.Categories;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Categorys.Commands.Create;

public class CreateCategoryCommandHandler(ICategoryCommandRepository CategoryCommandRepository, ICategoryQueryRepository categoryQueryRepository)
: ICommandHandler<CreateCategoryCommand, bool>
{
    public async Task<Result<bool>> Handle(
        CreateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!await categoryQueryRepository.IsUniqueAsync(x => x.Title == command.request.Title, true, cancellationToken))
                return Result.Error(MessageHelper.Format(AppMessages.Found, AppEntityCategory.Title));
            var category = command.request.Adapt<Category>();
            await CategoryCommandRepository.AddAsync(category, cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Category));
        }

        return true;
    }
}


public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.request)
    .SetValidator(new CreateCategoryRequestValidator());
    }
}
public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequestDto>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
              //.WithDisplayName(nameof(CreateCategoryRequestDto.Title), typeof(CreateCategoryRequestDto))
            .WithMessage(SharedValidationMessages.Required)
            .MaximumLength(250)
            .WithMessage(SharedValidationMessages.MaxLength);

        RuleFor(x => x.UrlName)
            .NotEmpty()
            .WithMessage("نام URL الزامی است.")
            .MaximumLength(250)
            .WithMessage("نام URL نمی‌تواند بیشتر از ۲۵۰ کاراکتر باشد.")
            .Matches("^[a-z0-9-]+$")
            .WithMessage("نام URL فقط می‌تواند شامل حروف انگلیسی کوچک، عدد و - باشد.");

        RuleFor(x => x.Image)
            .MaximumLength(250)
            .When(x => !string.IsNullOrWhiteSpace(x.Image));

        RuleFor(x => x.Icon)
            .MaximumLength(250)
            .When(x => !string.IsNullOrWhiteSpace(x.Icon));
    }
}
