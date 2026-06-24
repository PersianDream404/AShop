using Modules.Order.Domain.Entities;
using SharedKernel.Interface.Repositories;

namespace Modules.Order.Domain.Interfaces;

public interface IOrderCommandRepository: ICommandRepository<OrderEntity>
{

}

