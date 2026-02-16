using PizzaEnterprise.Domain.Common;
using PizzaEnterprise.Domain.Enums;
using PizzaEnterprise.Domain.Events;
using PizzaEnterprise.Domain.ValueObjects;

namespace PizzaEnterprise.Domain.Entities;

public class Payment : BaseEntity
{
    public Guid OrderId { get; set; }
    public Money Amount { get; set; } = Money.Zero;
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string? TransactionId { get; set; }
    public DateTime? CompletedAt { get; set; }

    public void Complete(string transactionId)
    {
        Status = PaymentStatus.Completed;
        TransactionId = transactionId;
        CompletedAt = DateTime.UtcNow;
        AddDomainEvent(new PaymentCompletedEvent(Id, OrderId));
    }

    public void Fail()
    {
        Status = PaymentStatus.Failed;
    }

    public void Refund()
    {
        Status = PaymentStatus.Refunded;
    }
}
