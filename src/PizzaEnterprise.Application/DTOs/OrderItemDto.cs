namespace PizzaEnterprise.Application.DTOs;

public record OrderItemDto(
    Guid Id,
    Guid PizzaId,
    string PizzaName,
    int Quantity,
    decimal UnitPrice,
    decimal Subtotal
);
