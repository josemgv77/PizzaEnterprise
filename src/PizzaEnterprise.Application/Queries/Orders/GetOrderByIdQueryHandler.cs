using AutoMapper;
using MediatR;
using PizzaEnterprise.Application.DTOs;
using PizzaEnterprise.Domain.Entities;
using PizzaEnterprise.Domain.Interfaces;

namespace PizzaEnterprise.Application.Queries.Orders;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Pizza> _pizzaRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(
        IRepository<Order> orderRepository,
        IRepository<Pizza> pizzaRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _pizzaRepository = pizzaRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
        {
            return null;
        }

        var pizzas = await _pizzaRepository.GetAllAsync(cancellationToken);
        var pizzaDict = pizzas.ToDictionary(p => p.Id, p => p.Name);

        var orderDto = _mapper.Map<OrderDto>(order);
        
        var items = order.Items.Select(item => new OrderItemDto(
            item.Id,
            item.PizzaId,
            pizzaDict.GetValueOrDefault(item.PizzaId, "Unknown"),
            item.Quantity,
            item.UnitPrice.Amount,
            item.Subtotal.Amount
        )).ToList();

        return orderDto with { Items = items };
    }
}
