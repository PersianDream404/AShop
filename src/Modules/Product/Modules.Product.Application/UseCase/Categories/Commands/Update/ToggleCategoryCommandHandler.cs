using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Modules.Product.Application.Contract.Interface.Categories;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.Categorys.Commands;
using Modules.Product.Domain.Interface;
using Modules.Product.Domain.Interface.Categories;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Categorys.Commands.Update;

public class ToggleCategoryCommandHandler(ICategoryCommandRepository categoryCommandRepository,ICategoryQueryRepository categoryQueryRepository)
: ICommandHandler<ToggleCategoryCommand, bool>
{
    public async Task<Result<bool>> Handle(
        ToggleCategoryCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var category = await categoryQueryRepository.GetByIdAsync(command.Id, cancellationToken);
            if (category == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.Category));

         
            await categoryCommandRepository.ToggleAsync(category,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Category));
        }

        return true;
    }
}
public class ToggleCategoryCommandValidator
    : AbstractValidator<ToggleCategoryCommand>
{
    public ToggleCategoryCommandValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ارسال شناسه الزامب است.");

    }
}