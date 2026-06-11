using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Mapster;
using Modules.Product.Application.Contract.Interface.Brands;
using Modules.Product.Application.Contract.UseCase.Brands.Commands;
using Modules.Product.Domain.Entities.Brands;
using Modules.Product.Domain.Interface.Brands;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Brands.Commands.Delete;

public class DeleteBrandCommandHandler(IBrandCommandRepository brandCommandRepository,IBrandQueryRepository brandQueryRepository)
: ICommandHandler<DeleteBrandCommand, bool>
{
    public async Task<Result<bool>> Handle(
        DeleteBrandCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var brand = await brandQueryRepository.GetByIdProjectedAsync(command.Id, cancellationToken);
            if (brand == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.Brand));
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Brand));
        }

        return true;
    }
}
public class DeleteBrandCommandValidator
    : AbstractValidator<DeleteBrandCommand>
{
    public DeleteBrandCommandValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ارسال شناسه الزامی است.");
   


    }
}