using MassTransit;
using Microsoft.Extensions.Logging;
using Sample.Ecommerce.Core.Domain;
using Sample.Ecommerce.Domain.Contracts.Orders;
using Sample.Ecommerce.Order.Core.Orders.Events;
using Sample.Ecommerce.Order.Core.Orders.EventStream;
using Sample.Ecommerce.Order.Core.Products;

namespace Sample.Ecommerce.Order.Core.Orders.Handlers;

internal sealed class SubmitOrderConsumer : IConsumer<SubmitOrder>    
{
    private readonly ILogger<SubmitOrderConsumer> _logger;    
    private readonly IOrderProcessorEvents _orderEvents;
    private readonly IProductRepository _productRepository;
    public SubmitOrderConsumer(ILogger<SubmitOrderConsumer> logger, 
        IOrderProcessorEvents orderEvents, IProductRepository productRepository)
    {
        _logger = logger;
        _orderEvents = orderEvents;
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<SubmitOrder> context)
    {        
        SubmitOrder submitOrder = context.Message;

        Product? product = await _productRepository.Get(submitOrder.Id).ConfigureAwait(false);

        if (product is null)
            throw new DomainException("Produto não encontrado.");

        OrderSubmitted orderCreated = new OrderSubmitted(submitOrder.Id, submitOrder.IdClient,
            product, submitOrder.DeliveryAddress, submitOrder.BankAccount, submitOrder.Amount);

        await _orderEvents.Include(orderCreated);
        _logger.LogInformation("Order {0} criada", submitOrder.Id);

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