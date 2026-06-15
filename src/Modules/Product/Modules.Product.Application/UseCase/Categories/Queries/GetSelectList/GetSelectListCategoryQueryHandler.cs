using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.Categorys.GetAll;
using Modules.Product.Application.Contract.Interface.Categories;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.Categorys.Queries;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Categorys.Queries.GetAll;

public class GetSelectListCategoryQueryHandler(ICategoryQueryRepository CategoryQueryRepository)
: IQueryHandler<GetSelectListCategoryQuery, PagedList<GetSelectListCategoryResponseDto>>
{
    public async Task<Result<PagedList<GetSelectListCategoryResponseDto>>> Handle(
        GetSelectListCategoryQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var result =await CategoryQueryRepository.GetSelectListProjectedAsync(query.request, cancellationToken);

            return result;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Category));
        }

    }
}
public class GetSelectListCategoryQueryValidator
    : AbstractValidator<GetSelectListCategoryQuery>
{
    public GetSelectListCategoryQueryValidator()
    {
        //RuleFor(x => x.request.Q)
        //    .NotEmpty()
        //    .WithMessage("Category Id is required");
    }
}
