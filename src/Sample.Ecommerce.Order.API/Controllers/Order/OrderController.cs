using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Sample.Ecommerce.Order.API.Moldes.Orders;
using Sample.Ecommerce.Domain.Contracts.Orders;
using Sample.Ecommerce.Domain.Contracts.Orders.Extensions;
using Sample.Ecommerce.Order.Core.Orders.Structs;

namespace Sample.Ecommerce.Order.API.Controllers.Order;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ISendEndpointProvider _endpointProvider;
    private readonly IOrderStructProcessor _orderProcessor;
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderController(ISendEndpointProvider endpointProvider, 
        IOrderStructProcessor orderProcessor, 
        IPublishEndpoint publishEndpoint)
    {
        _endpointProvider = endpointProvider;
        _orderProcessor = orderProcessor;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost("Create")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    public async Task<OrderResponse> CreateOrder([FromBody,Required] OrderCommand order, 
        CancellationToken cancellation = default)
    {
        SubmitOrder submit = new SubmitOrder(order.IdClient, order.IdProduct,
            order.DeliveryAddress, order.BankAccount, order.Amount);

        await _orderProcessor.Process(submit).ConfigureAwait(false);

        ISendEndpoint sendEndpoint = await _endpointProvider.GetSendEndpoint(submit.GetExchange());
        await sendEndpoint.Send(submit, cancellation).ConfigureAwait(false);

        return new OrderResponse(submit.Id, "Ordem criada e em aprovação.");
    }

    [HttpPost("Accept/{Id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    public async Task<OrderResponse> AcceptOrder(Guid Id, 
        CancellationToken cancellationToken = default)
    {
        await _publishEndpoint.Publish<IOrderAccepted>(new { Id }, cancellationToken);
        return new OrderResponse(Id, "Ordem aprovada.");
    }

    [HttpDelete("Refuse/{Id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    public async Task<OrderResponse> RefuseOrder(Guid Id,
        [FromBody,Required] string Message,
        CancellationToken cancellationToken = default)
    {
        await _publishEndpoint.Publish<IOrderRefused>(new { Id, Message }, cancellationToken);
        return new OrderResponse(Id, "Ordem reprovada.");
    }

    [HttpGet("Status/{Id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderResponse<OrderStatus>), StatusCodes.Status200OK)]
    public Task<OrderResponse<OrderStatus>> StatusOrder(Guid id, 
        CancellationToken cancellationToken = default)
    {
        var status = new OrderStatus("Em andamento.");
        return Task.FromResult(new OrderResponse<OrderStatus>(id, status));
    }

}
