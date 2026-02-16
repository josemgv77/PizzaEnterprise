namespace PizzaEnterprise.Application.DTOs;

public record PizzaDto(
    Guid Id,
    string Name,
    string Description,
    decimal BasePrice,
    string Size,
    bool IsAvailable,
    string? ImageUrl
);
