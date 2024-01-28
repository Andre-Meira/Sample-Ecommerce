using MassTransit;
using Sample.Ecommerce.Domain.Contracts.Orders;

namespace Sample.Ecommerce.Order.Core.Orders.Machine;

internal sealed class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public State Submitted { get; private set; }
    public State Accepted { get; private set; }
    public State Refused { get; private set; }
    public State Faulted { get; private set; }
    public State Completed { get; private set; }

    #pragma warning disable CS8618 
    public OrderStateMachine()
    #pragma warning restore CS8618 
    {
        InstanceState(e => e.CurrentState);

        Event(() => OrderSubmitted, x => x.CorrelateById(m => m.Message.Id));
        Event(() => OrderAccepted, x => x.CorrelateById(m => m.Message.Id));
        Event(() => OrderRefused, x => x.CorrelateById(m => m.Message.Id));
        Event(() => OrderFulfillmentCompleted, x => x.CorrelateById(m => m.Message.Id));
        Event(() => OrderFulfillmentFaulted, x => x.CorrelateById(m => m.Message.Id));

        Initially(
            When(OrderSubmitted)
                .Then(context =>
                {
                    context.Saga.CorrelationId = context.Message.Id;
                    context.Saga.Product = context.Message.Product;
                    context.Saga.IdClient = context.Message.IdClient;
                    context.Saga.Amount = context.Message.Amount;                    
                    context.Saga.BankAccount = context.Message.BankAccount;
                    context.Saga.DeliveryAddress = context.Message.DeliveryAddress;                                        
                })
                .TransitionTo(Submitted));

        During(Submitted,
            When(OrderRefused)
                .Then(context => context.Saga.Message = context.Message.Message)
                .TransitionTo(Refused),
            When(OrderAccepted)
                .Activity(e => e.OfType<AcceptOrderMachineActivity>())
                .TransitionTo(Accepted));

        During(Accepted,
            When(OrderFulfillmentFaulted)
                .Then(context => context.Saga.Message = context.Message.Message)
                .TransitionTo(Faulted),
            When(OrderFulfillmentCompleted)
                .TransitionTo(Completed));


    }

    public Event<IOrderSubmitted> OrderSubmitted { get; private set; }
    public Event<IOrderAccepted> OrderAccepted { get; private set; }
    public Event<IOrderRefused> OrderRefused { get; private set; }

    public Event<IOrderFulfillmentCompleted> OrderFulfillmentCompleted { get; private set; }
    public Event<IOrderFulfillmentFaulted> OrderFulfillmentFaulted { get; private set; }
}
 