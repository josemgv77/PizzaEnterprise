namespace PizzaEnterprise.Application.DTOs;

public record CustomerDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string FullName,
    AddressDto? DefaultAddress
);
