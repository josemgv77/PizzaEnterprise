using MediatR;
using Microsoft.Extensions.Logging;
using PizzaEnterprise.Domain.Events;

namespace PizzaEnterprise.Application.EventHandlers;

public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedEventHandler> _logger;

    public OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Order created: {OrderId}", notification.OrderId);
        
        return Task.CompletedTask;
    }
}
