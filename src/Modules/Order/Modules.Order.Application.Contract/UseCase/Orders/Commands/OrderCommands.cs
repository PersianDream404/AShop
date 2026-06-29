using Framwork.Bus.Command;
using Modules.Order.Application.Contract.DTOs;

namespace Modules.Order.Application.Contract.UseCase.Orders.Commands;

public record CreateOrderCommand(CreateOrderRequestDto Request) : ICommand<long>;
public record UpdateOrderStatusCommand(UpdateOrderStatusRequestDto Request) : ICommand<bool>;
public record UpdateTrackingNumberCommand(UpdateTrackingNumberRequestDto Request) : ICommand<bool>;
public record AddOrderItemCommand(long OrderId, CreateOrderItemRequestDto Request) : ICommand<bool>;
public record RemoveOrderItemCommand(long OrderItemId) : ICommand<bool>;
public record UpdateOrderItemCommand(long OrderId, UpdateOrderItemRequestDto Request) : ICommand<bool>;
public record UpdateOrderTotalAmountCommand(long OrderId) : ICommand<bool>;
public record PreparePaymentCommand(long OrderId, GetPreparePaymentRequestDto request) : ICommand<GetPreparePaymentResponseDto>;
