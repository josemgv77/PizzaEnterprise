using PizzaEnterprise.Domain.Common;
using PizzaEnterprise.Domain.ValueObjects;

namespace PizzaEnterprise.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid PizzaId { get; set; }
    public int Quantity { get; set; }
    public Money UnitPrice { get; set; } = Money.Zero;

    public Money Subtotal => UnitPrice.Multiply(Quantity);
}
