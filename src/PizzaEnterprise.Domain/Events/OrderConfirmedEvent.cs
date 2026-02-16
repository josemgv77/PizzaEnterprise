using PizzaEnterprise.Domain.Common;

namespace PizzaEnterprise.Domain.Events;

public class OrderConfirmedEvent : DomainEvent
{
    public Guid OrderId { get; }

    public OrderConfirmedEvent(Guid orderId)
    {
        OrderId = orderId;
    }
}
