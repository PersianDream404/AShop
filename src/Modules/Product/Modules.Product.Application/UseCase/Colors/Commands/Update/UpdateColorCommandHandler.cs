using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Mapster;
using Modules.Product.Application.Contract.Interface.Colors;
using Modules.Product.Application.Contract.UseCase.Colors.Commands;
using Modules.Product.Domain.Entities.Colors;
using Modules.Product.Domain.Interface.Colors;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Colors.Commands.Update;

public class UpdateColorCommandHandler(IColorCommandRepository colorCommandRepository,IColorQueryRepository colorQueryRepository)
: ICommandHandler<UpdateColorCommand, bool>
{
    public async Task<Result<bool>> Handle(
        UpdateColorCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var color = await colorQueryRepository.GetByIdAsync(command.request.Id, cancellationToken);
            if (color == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.Color));

            if (!await colorQueryRepository.IsUniqueAsync(x => x.ColorCode == command.request.ColorCode, false, cancellationToken))
                return Result.Error(MessageHelper.Format(AppMessages.Found, AppEntityColor.ColorCode));
            command.request.Adapt(color);
            await colorCommandRepository.UpdateAsync(color,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Color));
        }

        return true;
    }
}
public class UpdateColorCommandValidator
    : AbstractValidator<UpdateColorCommand>
{
    public UpdateColorCommandValidator()
    {

        RuleFor(x => x.request.Id)
            .NotEmpty()
            .WithMessage("ارسال شناسه الزامی است.");

        RuleFor(x => x.request.ColorCode)
            .NotEmpty()
            .WithMessage(" کد رنگ الزامی است.")
            .MaximumLength(250)
            .WithMessage("کد رنگ نمی‌تواند بیشتر از ۲۵۰ کاراکتر باشد.");

        RuleFor(x => x.request.ColorName)
            .NotEmpty()
            .WithMessage("  نام رنگ الزامی است.")
            .MaximumLength(250)
            .WithMessage("کد رنگ نمی‌تواند بیشتر از ۲۵۰ کاراکتر باشد.");
    }
}