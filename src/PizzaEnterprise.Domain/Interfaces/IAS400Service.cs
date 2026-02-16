namespace PizzaEnterprise.Domain.Interfaces;

public interface IAS400Service
{
    Task<bool> SyncOrderToAS400Async(Guid orderId, CancellationToken cancellationToken = default);
    Task<IEnumerable<dynamic>> GetCustomersFromAS400Async(CancellationToken cancellationToken = default);
}
