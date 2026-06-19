using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Modules.Product.Application.Contract.Interface.Categories;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.Categorys.Commands;
using Modules.Product.Domain.Interface;
using Modules.Product.Domain.Interface.Categories;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Categorys.Commands.Delete;

public class DeleteCategoryCommandHandler(ICategoryCommandRepository categoryCommandRepository,ICategoryQueryRepository categoryQueryRepository)
: ICommandHandler<DeleteCategoryCommand, bool>
{
    public async Task<Result<bool>> Handle(
        DeleteCategoryCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var category = await categoryQueryRepository.GetByIdProjectedAsync(command.Id, cancellationToken);
            if (category == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.Category));
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Category));
        }

        return true;
    }
}
public class DeleteCategoryCommandValidator
    : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty()
                  .WithMessage(SharedValidationMessages.Required);




    }
}