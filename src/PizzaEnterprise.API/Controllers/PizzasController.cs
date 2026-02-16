using MediatR;
using Microsoft.AspNetCore.Mvc;
using PizzaEnterprise.Application.Queries.Pizzas;

namespace PizzaEnterprise.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PizzasController : ControllerBase
{
    private readonly IMediator _mediator;

    public PizzasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("available")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAvailablePizzas()
    {
        var query = new GetAvailablePizzasQuery();
        var pizzas = await _mediator.Send(query);
        return Ok(pizzas);
    }
}
