using Mapster;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Domain.Entities;

namespace Modules.Order.Application.Mapping;

public class OrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ShoppingCart, ShoppingCartDto>()
            .Map(dest => dest, src => src);

        config.NewConfig<OrderEntity, OrderDto>()
            .Map(dest => dest, src => src);

        config.NewConfig<OrderItem, OrderItemDto>()
            .Map(dest => dest, src => src);
    }
}
