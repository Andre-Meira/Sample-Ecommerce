using MassTransit;
using Microsoft.Extensions.Logging;
using Sample.Ecommerce.Domain.Contracts.Orders;
using Sample.Ecommerce.Domain.Contracts.Orders.Extensions;
using Sample.Ecommerce.Order.Core.Orders.Events;
using Sample.Ecommerce.Order.Core.Orders.EventStream;

namespace Sample.Ecommerce.Order.Core.Orders.Machine;

internal sealed class AcceptOrderMachineActivity : IStateMachineActivity<OrderState, IOrderAccepted>
{
    private readonly ILogger<AcceptOrderMachineActivity> _logerr;
    private readonly IOrderProcessorEvents _orderEvents;

    public AcceptOrderMachineActivity(ILogger<AcceptOrderMachineActivity> logger
        , IOrderProcessorEvents orderProcessor)
    {
        _logerr = logger;
        _orderEvents = orderProcessor;
    }        

    public void Probe(ProbeContext context) => context.CreateScope("accept-order");

    public void Accept(StateMachineVisitor visitor) => visitor.Visit(this);


    public async Task Execute(BehaviorContext<OrderState, IOrderAccepted> context, 
        IBehavior<OrderState, IOrderAccepted> next)
    {
        await _orderEvents.Include(new OrderAccepted(context.Message.Id)).ConfigureAwait(false);

        _logerr.LogInformation("Process {0} id:{1}", nameof(FulfillOrder), context.CorrelationId);

        FulfillOrder fulfillOrder = new FulfillOrder(context.Saga.CorrelationId, 
            context.Saga.Product, context.Saga.DeliveryAddress,
            context.Saga.BankAccount, context.Saga.Amount);

        ISendEndpoint sendEndpoint = await context.GetSendEndpoint(fulfillOrder.GetExchange());        
        await sendEndpoint.Send<FulfillOrder>(fulfillOrder);

        await next.Execute(context).ConfigureAwait(false);
    }

    public Task Faulted<TException>(BehaviorExceptionContext<OrderState, IOrderAccepted, TException> context, 
        IBehavior<OrderState, IOrderAccepted> next) 
        where TException : Exception
    {
        return next.Faulted(context);
    }

}
