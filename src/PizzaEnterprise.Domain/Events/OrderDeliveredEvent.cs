using PizzaEnterprise.Domain.Common;

namespace PizzaEnterprise.Domain.Events;

public class OrderDeliveredEvent : DomainEvent
{
    public Guid OrderId { get; }

    public OrderDeliveredEvent(Guid orderId)
    {
        OrderId = orderId;
    }
}
