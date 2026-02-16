namespace PizzaEnterprise.Domain.Enums;

public enum OrderStatus
{
    Pending,
    Confirmed,
    Preparing,
    ReadyForDelivery,
    InDelivery,
    Delivered,
    Cancelled
}
