using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Product.Application.Contract.Interface.Categories;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.Categorys.Commands;
using Modules.Product.Domain.Interface;
using Modules.Product.Domain.Interface.Categories;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Categorys.Commands.Update;

public class UpdateCategoryCommandHandler(ICategoryCommandRepository categoryCommandRepository, ICategoryQueryRepository categoryQueryRepository)
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
            await categoryCommandRepository.UpdateAsync(category, cancellationToken);

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
            .WithMessage(SharedValidationMessages.Required);

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
            .WithMessage(SharedValidationMessages.MaxLength)
            .When(x => !string.IsNullOrWhiteSpace(x.request.Image));

        RuleFor(x => x.request.Icon)
            .MaximumLength(250)
            .WithMessage(SharedValidationMessages.MaxLength)
            .When(x => !string.IsNullOrWhiteSpace(x.request.Icon));
    }
}
