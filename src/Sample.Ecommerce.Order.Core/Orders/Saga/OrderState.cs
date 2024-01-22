using MassTransit;
using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Order.Core.Orders.Machine;

internal sealed class OrderState : SagaStateMachineInstance, ISagaVersion
{
    public int Version { get ; set ; }
    public Guid CorrelationId { get ; set ; }

    public string CurrentState { get; set; } = null!;
    public string? Message { get; set; } 

    public Guid IdClient { get; set; }
    public Guid IdProduct { get; set; }    

    public BaseAddress DeliveryAddress { get; set; } = null!;
    public BaseBankAccount BankAccount { get; set; } = null!;    

    public int Amount { get; set; }
    public decimal Value { get; set; }
}
