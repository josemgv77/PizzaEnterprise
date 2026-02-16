using MediatR;
using Microsoft.Extensions.Logging;
using PizzaEnterprise.Domain.Events;
using PizzaEnterprise.Domain.Interfaces;

namespace PizzaEnterprise.Application.EventHandlers;

public class OrderConfirmedEventHandler : INotificationHandler<OrderConfirmedEvent>
{
    private readonly ILogger<OrderConfirmedEventHandler> _logger;
    private readonly IAS400Service _as400Service;

    public OrderConfirmedEventHandler(
        ILogger<OrderConfirmedEventHandler> logger,
        IAS400Service as400Service)
    {
        _logger = logger;
        _as400Service = as400Service;
    }

    public async Task Handle(OrderConfirmedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Order confirmed: {OrderId}", notification.OrderId);
        
        await _as400Service.SyncOrderToAS400Async(notification.OrderId, cancellationToken);
    }
}
