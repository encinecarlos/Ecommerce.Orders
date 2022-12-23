using System.Net.Mime;
using ECommerce.Orders.Api.Application.Command.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Orders.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class OrdersController : ControllerBase
{
    private IMediator Mediator { get; }

    public OrdersController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpGet]
    
    public IActionResult GetOrders()
    {
        return Ok(Guid.NewGuid().ToString());
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderResponse>> CreateNewOrder([FromBody] OrderRequest request,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}