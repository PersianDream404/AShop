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

public class GetColorQueryHandler(IColorQueryRepository ColorQueryRepository)
: IQueryHandler<GetAllColorQuery, PagedList<GetAllColorResponseDto>>
{
    public async Task<Result<PagedList<GetAllColorResponseDto>>> Handle(
        GetAllColorQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var result =await ColorQueryRepository.GetAllProjectedAsync(query.request, cancellationToken);

            return result;
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
