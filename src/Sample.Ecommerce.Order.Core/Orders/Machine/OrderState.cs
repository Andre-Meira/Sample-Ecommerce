using MassTransit;
using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Order.Core.Orders.Machine;

internal sealed class OrderState : SagaStateMachineInstance, ISagaVersion
{
    public int Version { get ; set ; }
    public Guid CorrelationId { get ; set ; }

    public string CurrentState { get; set; } = null!;
    public string? FaultReason { get; set; } 

    public Guid IdClient { get; set; }
    public Guid IdProduct { get; set; }    

    public Address DeliveryAddress { get; set; } = null!;
    public BankAccount BankAccount { get; set; } = null!;    

    public decimal Amount { get; set; }
    public decimal Value { get; set; }
}
