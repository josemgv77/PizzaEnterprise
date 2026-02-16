using PizzaEnterprise.Domain.Common;
using PizzaEnterprise.Domain.Enums;
using PizzaEnterprise.Domain.ValueObjects;

namespace PizzaEnterprise.Domain.Entities;

public class Pizza : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Money BasePrice { get; set; } = Money.Zero;
    public PizzaSize Size { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string? ImageUrl { get; set; }
}
