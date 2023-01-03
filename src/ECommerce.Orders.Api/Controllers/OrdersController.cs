using System.Net.Mime;
using ECommerce.Orders.Api.Application.Command.Orders;
using ECommerce.Orders.Api.Application.Query.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Orders.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class OrdersController : ControllerBase
{
    private IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    
    public async Task<ActionResult<GetOrdersDto>> GetOrders()
    {
        var result = await _mediator.Send(new GetOrdersQuery(), CancellationToken.None);
        return result;
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderDto>> CreateNewOrder([FromBody] AddOrderCommand request,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}