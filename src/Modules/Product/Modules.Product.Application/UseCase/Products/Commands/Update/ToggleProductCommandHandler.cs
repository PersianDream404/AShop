using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Product.Application.Contract.Interface.Products;
using Modules.Product.Application.Contract.UseCase.Products.Commands;
using Modules.Product.Domain.Entities.Products;
using Modules.Product.Domain.Interface.Products;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Products.Commands.Update;

public class ToggleProductCommandHandler(IProductCommandRepository productCommandRepository,IProductQueryRepository productQueryRepository)
: ICommandHandler<ToggleProductCommand, bool>
{
    public async Task<Result<bool>> Handle(
        ToggleProductCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var product = await productQueryRepository.GetByIdAsync(command.Id, cancellationToken);
            if (product == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.Product));

         
            await productCommandRepository.ToggleAsync(product,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Product));
        }

        return true;
    }
}
public class ToggleProductCommandValidator
    : AbstractValidator<ToggleProductCommand>
{
    public ToggleProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required);

    }
}