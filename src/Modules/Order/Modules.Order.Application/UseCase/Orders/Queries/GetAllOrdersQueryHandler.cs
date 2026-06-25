using Ardalis.Result;
using Framwork.Bus.Query;
using Mapster;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Queries;

namespace Modules.Order.Application.UseCase.Orders.Queries;

public class GetAllOrdersQueryHandler(IOrderQueryRepository queryRepository)
    : IQueryHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{
    public async Task<Result<IEnumerable<OrderDto>>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var orders = await queryRepository.GetAllAsync(cancellationToken);
            return Result.Success(orders.Adapt<IEnumerable<OrderDto>>());
        }
        catch (Exception ex)
        {
            return Result.Error($"Error retrieving orders: {ex.Message}");
        }
    }
}
