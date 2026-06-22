using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Product.Application.Contract.Interface.Products;
using Modules.Product.Application.Contract.Resources.Products;
using Modules.Product.Application.Contract.UseCase.Products.Commands;
using Modules.Product.Domain.Entities.Products;
using Modules.Product.Domain.Interface.Products;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Products.Commands.Update;

public class UpdateProductCommandHandler(IProductCommandRepository productCommandRepository,IProductQueryRepository productQueryRepository)
: ICommandHandler<UpdateProductCommand, bool>
{
    public async Task<Result<bool>> Handle(
        UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var product = await productQueryRepository.GetByIdAsync(command.request.Id, cancellationToken);
            if (product == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.Product));

            if (!await productQueryRepository.IsUniqueAsync(x => x.Title == command.request.Title, false, cancellationToken))
                return Result.Error(MessageHelper.Format(AppMessages.Found, ProductFieldNames.Title));
            command.request.Adapt(product);
            await productCommandRepository.UpdateAsync(product,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Product));
        }

        return true;
    }
}
public class UpdateProductCommandValidator
    : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {

        RuleFor(x => x.request.Id)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required);


    }
}