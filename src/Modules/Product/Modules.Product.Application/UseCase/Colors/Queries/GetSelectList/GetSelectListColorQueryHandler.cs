using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.Colors.GetAll;
using Modules.Product.Application.Contract.Interface.Colors;
using Modules.Product.Application.Contract.UseCase.Colors.Queries;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Colors.Queries.GetAll;

public class GetSelectListColorQueryHandler(IColorQueryRepository ColorQueryRepository)
: IQueryHandler<GetSelectListColorQuery, PagedList<GetSelectListColorResponseDto>>
{
    public async Task<Result<PagedList<GetSelectListColorResponseDto>>> Handle(
        GetSelectListColorQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var result =await ColorQueryRepository.GetSelectListProjectedAsync(query.request, cancellationToken);

            return result;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Color));
        }

    }
}
public class GetSelectListColorQueryValidator
    : AbstractValidator<GetSelectListColorQuery>
{
    public GetSelectListColorQueryValidator()
    {
        //RuleFor(x => x.request.Q)
        //    .NotEmpty()
        //    .WithMessage("Color Id is required");
    }
}
