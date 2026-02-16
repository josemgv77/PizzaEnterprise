using PizzaEnterprise.Domain.Common;

namespace PizzaEnterprise.Domain.Events;

public class OrderCreatedEvent : DomainEvent
{
    public Guid OrderId { get; }

    public OrderCreatedEvent(Guid orderId)
    {
        OrderId = orderId;
    }
}
