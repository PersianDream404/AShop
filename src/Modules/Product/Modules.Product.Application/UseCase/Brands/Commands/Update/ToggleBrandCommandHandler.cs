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

namespace Modules.Product.Application.UseCase.Brands.Commands.Update;

public class ToggleBrandCommandHandler(IBrandCommandRepository brandCommandRepository,IBrandQueryRepository brandQueryRepository)
: ICommandHandler<ToggleBrandCommand, bool>
{
    public async Task<Result<bool>> Handle(
        ToggleBrandCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var brand = await brandQueryRepository.GetByIdAsync(command.Id, cancellationToken);
            if (brand == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.Brand));

         
            await brandCommandRepository.ToggleAsync(brand,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Brand));
        }

        return true;
    }
}
public class ToggleBrandCommandValidator
    : AbstractValidator<ToggleBrandCommand>
{
    public ToggleBrandCommandValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ارسال شناسه الزامب است.");

    }
}