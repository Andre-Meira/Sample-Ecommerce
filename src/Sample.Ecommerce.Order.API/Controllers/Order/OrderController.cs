using MassTransit;
using Sample.Ecommerce.Domain.Contracts.Orders.Extensions;
using Microsoft.AspNetCore.Mvc;
using Sample.Ecommerce.Domain.Contracts.Orders;
using Sample.Ecommerce.Order.API.Moldes;
using System.ComponentModel.DataAnnotations;

namespace Sample.Ecommerce.Order.API.Controllers.Order;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ISendEndpointProvider _endpointProvider;

    public OrderController(ISendEndpointProvider sendEndpoint)
    {
        _endpointProvider = sendEndpoint;   
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    public async Task<OrderResponse> CreateOrder([FromBody,Required] OrderCommand order, 
        CancellationToken cancellation)
    {
        Guid orderId = Guid.NewGuid();
        
        SubmitOrder submit = new SubmitOrder(orderId, order.IdClient, order.IdProduct,
            DateTime.Now, order.DeliveryAddress, order.BankAccount,order.Amount, order.Value);

        ISendEndpoint sendEndpoint = await _endpointProvider.GetSendEndpoint(submit.GetExchange());
        await sendEndpoint.Send(submit).ConfigureAwait(false);

        return new OrderResponse(orderId, "Ordem criado e em processo.");
    }
}
