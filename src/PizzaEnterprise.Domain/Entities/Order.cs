using PizzaEnterprise.Domain.Common;
using PizzaEnterprise.Domain.Enums;
using PizzaEnterprise.Domain.Events;
using PizzaEnterprise.Domain.ValueObjects;

namespace PizzaEnterprise.Domain.Entities;

public class Order : BaseEntity
{
    public string OrderNumber { get; private set; } = string.Empty;
    public Guid CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public Money TotalAmount { get; private set; } = Money.Zero;
    public Address DeliveryAddress { get; private set; } = null!;
    public Guid? DeliveryPersonId { get; private set; }
    public Guid? PaymentId { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    private Order()
    {
    }

    public static Order Create(Customer customer, Address deliveryAddress, List<OrderItem> items)
    {
        var order = new Order
        {
            OrderNumber = GenerateOrderNumber(),
            CustomerId = customer.Id,
            DeliveryAddress = deliveryAddress,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending
        };

        foreach (var item in items)
        {
            item.OrderId = order.Id;
            order._items.Add(item);
        }

        order.RecalculateTotalAmount();
        order.AddDomainEvent(new OrderCreatedEvent(order.Id));

        return order;
    }

    public void AddItem(OrderItem item)
    {
        item.OrderId = Id;
        _items.Add(item);
        RecalculateTotalAmount();
    }

    public void RemoveItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            _items.Remove(item);
            RecalculateTotalAmount();
        }
    }

    public void Confirm()
    {
        Status = OrderStatus.Confirmed;
        AddDomainEvent(new OrderConfirmedEvent(Id));
    }

    public void StartPreparation()
    {
        Status = OrderStatus.Preparing;
    }

    public void MarkReadyForDelivery()
    {
        Status = OrderStatus.ReadyForDelivery;
    }

    public void AssignDeliveryPerson(Guid deliveryPersonId)
    {
        DeliveryPersonId = deliveryPersonId;
        Status = OrderStatus.InDelivery;
    }

    public void CompleteDelivery()
    {
        Status = OrderStatus.Delivered;
        AddDomainEvent(new OrderDeliveredEvent(Id));
    }

    public void Cancel()
    {
        Status = OrderStatus.Cancelled;
    }

    private void RecalculateTotalAmount()
    {
        if (_items.Count == 0)
        {
            TotalAmount = Money.Zero;
            return;
        }

        var total = _items[0].Subtotal;
        for (int i = 1; i < _items.Count; i++)
        {
            total = total.Add(_items[i].Subtotal);
        }

        TotalAmount = total;
    }

    private static string GenerateOrderNumber()
    {
        var date = DateTime.UtcNow;
        var randomNumber = Random.Shared.Next(1000, 9999);
        return $"ORD-{date:yyyyMMdd}-{randomNumber}";
    }
}
