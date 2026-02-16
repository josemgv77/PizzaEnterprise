using MediatR;
using Microsoft.Extensions.Logging;
using PizzaEnterprise.Domain.Events;

namespace PizzaEnterprise.Application.EventHandlers;

public class PaymentCompletedEventHandler : INotificationHandler<PaymentCompletedEvent>
{
    private readonly ILogger<PaymentCompletedEventHandler> _logger;

    public PaymentCompletedEventHandler(ILogger<PaymentCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PaymentCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Payment completed: {PaymentId} for Order: {OrderId}", 
            notification.PaymentId, 
            notification.OrderId);
        
        return Task.CompletedTask;
    }
}
