using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.Validation.Resources;
using Modules.Product.Application.Contract.DTOs.Products.Get;
using Modules.Product.Application.Contract.DTOs.Products.GetAll;
using Modules.Product.Application.Contract.Interface.Products;
using Modules.Product.Application.Contract.UseCase.Products.Queries;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Products.Queries.Get;

public class GetByIdProductQueryHandler(IProductQueryRepository ProductQueryRepository)
: IQueryHandler<GetByIdProductQuery, GetByIdProductResponseDto>
{
    public async Task<Result<GetByIdProductResponseDto>> Handle(
        GetByIdProductQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var brand = await ProductQueryRepository.GetByIdProjectedAsync(query.Id, cancellationToken);
            if (brand == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.Product));
            return brand;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Product));
        }

    }
}
public class GetAllProductQueryValidator
    : AbstractValidator<GetByIdProductQuery>
{
    public GetAllProductQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required);
    }
}
