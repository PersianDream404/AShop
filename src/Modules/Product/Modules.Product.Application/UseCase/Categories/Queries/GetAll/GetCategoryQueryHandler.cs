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

public class GetCategoryQueryHandler(ICategoryQueryRepository CategoryQueryRepository)
: IQueryHandler<GetAllCategoryQuery, PagedList<GetAllCategoryResponseDto>>
{
    public async Task<Result<PagedList<GetAllCategoryResponseDto>>> Handle(
        GetAllCategoryQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var result =await CategoryQueryRepository.GetAllProjectedAsync(query.request, cancellationToken);

            return result;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Category));
        }

    }
}
public class GetAllCategoryQueryValidator
    : AbstractValidator<GetAllCategoryQuery>
{
    public GetAllCategoryQueryValidator()
    {
        //RuleFor(x => x.request.Q)
        //    .NotEmpty()
        //    .WithMessage("Category Id is required");
    }
}
