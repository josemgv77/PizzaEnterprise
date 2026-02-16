using MediatR;
using PizzaEnterprise.Domain.Entities;
using PizzaEnterprise.Domain.Exceptions;
using PizzaEnterprise.Domain.Interfaces;
using PizzaEnterprise.Domain.ValueObjects;

namespace PizzaEnterprise.Application.Commands.Orders;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<Pizza> _pizzaRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IRepository<Customer> customerRepository,
        IRepository<Pizza> pizzaRepository,
        IRepository<Order> orderRepository,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _pizzaRepository = pizzaRepository;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer == null)
        {
            throw new DomainException($"Customer with ID {request.CustomerId} not found");
        }

        var pizzaIds = request.Items.Select(i => i.PizzaId).Distinct().ToList();
        var pizzas = await _pizzaRepository.GetAllAsync(cancellationToken);
        var pizzaDict = pizzas.Where(p => pizzaIds.Contains(p.Id)).ToDictionary(p => p.Id);

        foreach (var item in request.Items)
        {
            if (!pizzaDict.TryGetValue(item.PizzaId, out var pizza))
            {
                throw new DomainException($"Pizza with ID {item.PizzaId} not found");
            }

            if (!pizza.IsAvailable)
            {
                throw new DomainException($"Pizza {pizza.Name} is not available");
            }
        }

        var deliveryAddress = Address.Create(
            request.DeliveryAddress.Street,
            request.DeliveryAddress.City,
            request.DeliveryAddress.State,
            request.DeliveryAddress.ZipCode,
            request.DeliveryAddress.Country
        );

        var orderItems = request.Items.Select(item =>
        {
            // This is safe because we validated all pizzas exist above
            if (!pizzaDict.TryGetValue(item.PizzaId, out var pizza))
            {
                throw new InvalidOperationException($"Pizza {item.PizzaId} not found after validation.");
            }
            
            return new OrderItem
            {
                PizzaId = item.PizzaId,
                Quantity = item.Quantity,
                UnitPrice = pizza.BasePrice
            };
        }).ToList();

        var order = Order.Create(customer, deliveryAddress, orderItems);

        await _orderRepository.AddAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
