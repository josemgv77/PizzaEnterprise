using MediatR;
using Microsoft.AspNetCore.Mvc;
using PizzaEnterprise.Application.Commands.Orders;
using PizzaEnterprise.Application.Queries.Orders;

namespace PizzaEnterprise.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var orderId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, orderId);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var query = new GetOrderByIdQuery(id);
        var order = await _mediator.Send(query);
        
        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpPost("{id}/confirm")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmOrder(Guid id)
    {
        var command = new ConfirmOrderCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
