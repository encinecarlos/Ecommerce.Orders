using ECommerce.Orders.Api.Application.Command.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Orders.Api.Controllers;

[ApiController]
[Route("[controller]")]
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
    public async Task<ActionResult<OrderResponse>> CreateNewOrder([FromBody] OrderRequest request,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}