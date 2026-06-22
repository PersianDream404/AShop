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

namespace Modules.Product.Application.UseCase.Products.Commands.Delete;

public class DeleteProductCommandHandler(IProductCommandRepository productCommandRepository,IProductQueryRepository productQueryRepository)
: ICommandHandler<DeleteProductCommand, bool>
{
    public async Task<Result<bool>> Handle(
        DeleteProductCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var product = await productQueryRepository.GetByIdProjectedAsync(command.Id, cancellationToken);
            if (product == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.Product));
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Product));
        }

        return true;
    }
}
public class DeleteProductCommandValidator
    : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required);


    }
}