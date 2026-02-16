using MediatR;
using PizzaEnterprise.Application.DTOs;

namespace PizzaEnterprise.Application.Queries.Pizzas;

public record GetAvailablePizzasQuery : IRequest<IEnumerable<PizzaDto>>;
