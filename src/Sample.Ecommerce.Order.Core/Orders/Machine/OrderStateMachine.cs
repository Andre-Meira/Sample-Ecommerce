using MassTransit;
using Sample.Ecommerce.Domain.Contracts.Orders;

namespace Sample.Ecommerce.Order.Core.Orders.Machine;

internal sealed class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public State Submitted { get; private set; }
    public State Accepted { get; private set; }
    public State Canceled { get; private set; }
    public State Faulted { get; private set; }
    public State Completed { get; private set; }

    #pragma warning disable CS8618 
    public OrderStateMachine()
    #pragma warning restore CS8618 
    {
        InstanceState(e => e.CurrentState);
    }

    public Event<IOrderSubmitted> OrderSubmitted { get; private set; }
    public Event<IOrderAccepted> OrderAccepted { get; private set; }
}
