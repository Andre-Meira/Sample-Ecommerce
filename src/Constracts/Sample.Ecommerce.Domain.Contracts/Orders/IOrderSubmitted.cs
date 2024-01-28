using MassTransit;
using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Domain.Contracts.Orders;

[EntityName(nameof(IOrderSubmitted))]
public interface IOrderSubmitted
{
    public Guid Id { get; init; }
    public Guid IdClient { get; init; }    
    public DateTime Date { get; init; }

    public BaseAddress DeliveryAddress { get; init; } 
    public BaseBankAccount BankAccount { get; init; }    
    public BaseProduct Product { get; init; }

    public int Amount { get; init; }    
}
