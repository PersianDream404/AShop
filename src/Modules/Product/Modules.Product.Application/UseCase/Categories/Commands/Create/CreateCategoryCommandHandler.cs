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
        RuleFor(x => x.request.Title)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required)
            .MaximumLength(250)
            .WithMessage(SharedValidationMessages.MaxLength);

        RuleFor(x => x.request.UrlName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required)
            .MaximumLength(250)
            .WithMessage(SharedValidationMessages.MaxLength)
            .Matches("^[a-z0-9-]+$")
            .WithMessage(SharedValidationMessages.UrlSlugPattern);

        RuleFor(x => x.request.Image)
            .MaximumLength(250)
            .When(x => !string.IsNullOrWhiteSpace(x.request.Image))
            .WithMessage(SharedValidationMessages.MaxLength)
            ;

        RuleFor(x => x.request.Icon)
            .MaximumLength(250)
            .When(x => !string.IsNullOrWhiteSpace(x.request.Icon))
            .WithMessage(SharedValidationMessages.MaxLength);
    }
}
