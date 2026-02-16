namespace PizzaEnterprise.Application.DTOs;

public record OrderDto(
    Guid Id,
    string OrderNumber,
    Guid CustomerId,
    DateTime OrderDate,
    string Status,
    decimal TotalAmount,
    AddressDto DeliveryAddress,
    Guid? DeliveryPersonId,
    Guid? PaymentId,
    List<OrderItemDto> Items
);
