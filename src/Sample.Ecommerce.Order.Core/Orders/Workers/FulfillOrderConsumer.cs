using MassTransit;
using MassTransit.Courier.Contracts;
using Sample.Ecommerce.Domain.Contracts.Orders;
using Sample.Ecommerce.Order.Core.Orders.Machine.Activity;

namespace Sample.Ecommerce.Order.Core.Orders.Workers;

internal sealed class FulfillOrderConsumer : IConsumer<FulfillOrder>
{
    public async Task Consume(ConsumeContext<FulfillOrder> context)
    {
        RoutingSlipBuilder routing = new RoutingSlipBuilder(Guid.NewGuid());

        var inventoryArguments = new AllocateInventoryArguments(context.Message.Id,
            context.Message.Product.Id, context.Message.Amount);

        routing.AddActivity(AllocateInventoryActivity.Name, AllocateInventoryActivity.Endpoint, inventoryArguments);

        var processPayment = new ProcessPaymentArgunments(context.Message.Id, context.Message.BankAccount);
        routing.AddActivity(ProcessPaymentActivity.Name, ProcessPaymentActivity.Endpoint, processPayment);

        await routing.AddSubscription(context.SourceAddress, RoutingSlipEvents.Faulted,
            RoutingSlipEventContents.Data, e => Faulted(context));

        await routing.AddSubscription(context.SourceAddress, RoutingSlipEvents.ActivityCompleted,
            RoutingSlipEventContents.None, ProcessPaymentActivity.Name,
            e => Completed(context));

        routing.Build();
    }

    private async Task Faulted(ConsumeContext<FulfillOrder> context)
    {
        await context.Publish<IOrderFulfillmentFaulted>(new
        {
            context.Message.Id,
            Message = "Não foi possivel concluir o pagamento."

        }).ConfigureAwait(false);
    }

    private async Task Completed(ConsumeContext<FulfillOrder> context)
    {
        await context.Publish<IOrderFulfillmentCompleted>(new {context.Message.Id})
            .ConfigureAwait(false);
    }
}

internal sealed class FulfillOrderConsumerDefinition : ConsumerDefinition<FulfillOrderConsumer>
{
    public FulfillOrderConsumerDefinition()
    {
        EndpointName = "queue-order";
    }

    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<FulfillOrderConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);
    }
}
