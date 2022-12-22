using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Orders.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetOrders()
    {
        return Ok(Guid.NewGuid().ToString());
    }
}