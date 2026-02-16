using MediatR;
using PizzaEnterprise.Domain.Entities;
using PizzaEnterprise.Domain.Exceptions;
using PizzaEnterprise.Domain.Interfaces;

namespace PizzaEnterprise.Application.Commands.Orders;

public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, Unit>
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmOrderCommandHandler(
        IRepository<Order> orderRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
        {
            throw new DomainException($"Order with ID {request.OrderId} not found");
        }

        order.Confirm();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
