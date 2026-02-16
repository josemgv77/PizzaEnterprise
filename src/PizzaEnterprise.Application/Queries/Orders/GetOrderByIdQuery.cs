using MediatR;
using PizzaEnterprise.Application.DTOs;

namespace PizzaEnterprise.Application.Queries.Orders;

public record GetOrderByIdQuery(Guid OrderId) : IRequest<OrderDto?>;
