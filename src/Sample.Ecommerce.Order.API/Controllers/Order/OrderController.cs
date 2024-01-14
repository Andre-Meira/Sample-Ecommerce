using Microsoft.AspNetCore.Mvc;
using Sample.Ecommerce.Order.API.Moldes;
using System.ComponentModel.DataAnnotations;

namespace Sample.Ecommerce.Order.API.Controllers.Order;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    public Task<OrderResponse> CreateOrder([FromBody,Required] OrderCommand order, 
        CancellationToken cancellation)
    {
        Guid orderId = Guid.NewGuid();

        return Task.FromResult(new OrderResponse(orderId, "Ordem criado e em processo."));
    }
}
