using Modules.Order.Application.Contract.DTOs;
using System.Linq.Expressions;

namespace Modules.Order.Persistence.Mapper.Orders;

public static class OrderMapper
{
    public static Expression<Func<Domain.Entities.OrderEntity, OrderDto>> ToGetByIdDto()
    {
        return x => new OrderDto
        {
            Id = x.Id,
        };
    }


}
