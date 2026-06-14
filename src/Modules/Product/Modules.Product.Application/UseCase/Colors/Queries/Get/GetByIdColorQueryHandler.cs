using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Modules.Product.Application.Contract.DTOs.Colors.GetAll;
using Modules.Product.Application.Contract.Interface.Colors;
using Modules.Product.Application.Contract.UseCase.Colors.Queries;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Colors.Queries.Get;

public class GetByIdColorQueryHandler(IColorQueryRepository ColorQueryRepository)
: IQueryHandler<GetByIdColorQuery, GetByIdColorResponseDto>
{
    public async Task<Result<GetByIdColorResponseDto>> Handle(
        GetByIdColorQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var color = await ColorQueryRepository.GetByIdProjectedAsync(query.Id, cancellationToken);
            if (color == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.Color));
            return color;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Color));
        }

    }
}
public class GetAllColorQueryValidator
    : AbstractValidator<GetAllColorQuery>
{
    public GetAllColorQueryValidator()
    {
        //RuleFor(x => x.request.Q)
        //    .NotEmpty()
        //    .WithMessage("Color Id is required");
    }
}
