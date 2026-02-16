using MediatR;

namespace PizzaEnterprise.Application.Commands.Orders;

public record ConfirmOrderCommand(Guid OrderId) : IRequest<Unit>;
