using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.Validation.Resources;
using Modules.Product.Application.Contract.DTOs.Categorys.GetAll;
using Modules.Product.Application.Contract.Interface.Categories;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.Categorys.Queries;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Categorys.Queries.Get;

public class GetByIdCategoryQueryHandler(ICategoryQueryRepository CategoryQueryRepository)
: IQueryHandler<GetByIdCategoryQuery, GetByIdCategoryResponseDto>
{
    public async Task<Result<GetByIdCategoryResponseDto>> Handle(
        GetByIdCategoryQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var category = await CategoryQueryRepository.GetByIdProjectedAsync(query.Id, cancellationToken);
            if (category == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.Category));
            return category;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Category));
        }

    }
}
public class GetAllCategoryQueryValidator
    : AbstractValidator<GetByIdCategoryQuery>
{
    public GetAllCategoryQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required);
    }
}
