using PizzaEnterprise.Domain.Common;

namespace PizzaEnterprise.Domain.Entities;

public class DeliveryPerson : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string VehiclePlate { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;
    public bool IsActive { get; set; } = true;

    public string FullName => $"{FirstName} {LastName}";
}
