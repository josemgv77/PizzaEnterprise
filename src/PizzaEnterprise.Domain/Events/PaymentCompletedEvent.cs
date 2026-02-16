using PizzaEnterprise.Domain.Common;

namespace PizzaEnterprise.Domain.Events;

public class PaymentCompletedEvent : DomainEvent
{
    public Guid PaymentId { get; }
    public Guid OrderId { get; }

    public PaymentCompletedEvent(Guid paymentId, Guid orderId)
    {
        PaymentId = paymentId;
        OrderId = orderId;
    }
}
