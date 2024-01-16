using MassTransit;
using Microsoft.Extensions.Logging;
using Sample.Ecommerce.Domain.Contracts.Orders;
using Sample.Ecommerce.Order.Core.Orders.Events;
using Sample.Ecommerce.Order.Core.Orders.EventStream;
using Sample.Ecommerce.Order.Core.Orders.Structs;

namespace Sample.Ecommerce.Order.Core.Orders.Handlers;

internal sealed class SubmitOrderConsumer : IConsumer<SubmitOrder>    
{
    private readonly ILogger<SubmitOrderConsumer> _logger;
    private readonly IOrderStructProcessor _orderStructProcessor;
    private readonly IOrderProcessorEvents _orderEvents;

    public SubmitOrderConsumer(ILogger<SubmitOrderConsumer> logger, 
        IOrderStructProcessor orderStructProcessor, IOrderProcessorEvents orderEvents)
    {
        _logger = logger;
        _orderStructProcessor = orderStructProcessor;
        _orderEvents = orderEvents;
    }

    public async Task Consume(ConsumeContext<SubmitOrder> context)
    {        
        SubmitOrder submitOrder = context.Message;

        await _orderStructProcessor.Process(submitOrder);

        OrderSubmitted orderCreated = new OrderSubmitted(submitOrder.Id,submitOrder.IdClient, 
            submitOrder.IdProduct, submitOrder.DeliveryAddress, submitOrder.BankAccount,
            submitOrder.Amount, submitOrder.Value);

        await _orderEvents.Include(orderCreated);
        _logger.LogInformation("Order {0} criado", submitOrder.Id);

        await context.Publish<IOrderSubmitted>(orderCreated);
    }    
}


internal sealed class SubmitOrderConsumerDefinition : ConsumerDefinition<SubmitOrderConsumer>
{
    public SubmitOrderConsumerDefinition()
    {
        EndpointName = "queue-order";
    }

    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<SubmitOrderConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);
    }
}