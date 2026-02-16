using MediatR;
using PizzaEnterprise.Application.DTOs;

namespace PizzaEnterprise.Application.Commands.Orders;

public record CreateOrderCommand(
    Guid CustomerId,
    AddressDto DeliveryAddress,
    List<CreateOrderItemDto> Items
) : IRequest<Guid>;

public record CreateOrderItemDto(
    Guid PizzaId,
    int Quantity
);
