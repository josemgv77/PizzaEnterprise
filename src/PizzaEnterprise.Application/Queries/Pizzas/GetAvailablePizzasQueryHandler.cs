using AutoMapper;
using MediatR;
using PizzaEnterprise.Application.DTOs;
using PizzaEnterprise.Domain.Entities;
using PizzaEnterprise.Domain.Interfaces;

namespace PizzaEnterprise.Application.Queries.Pizzas;

public class GetAvailablePizzasQueryHandler : IRequestHandler<GetAvailablePizzasQuery, IEnumerable<PizzaDto>>
{
    private readonly IRepository<Pizza> _pizzaRepository;
    private readonly IMapper _mapper;

    public GetAvailablePizzasQueryHandler(
        IRepository<Pizza> pizzaRepository,
        IMapper mapper)
    {
        _pizzaRepository = pizzaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PizzaDto>> Handle(GetAvailablePizzasQuery request, CancellationToken cancellationToken)
    {
        var pizzas = await _pizzaRepository.GetAllAsync(cancellationToken);
        var availablePizzas = pizzas.Where(p => p.IsAvailable);
        
        return _mapper.Map<IEnumerable<PizzaDto>>(availablePizzas);
    }
}
