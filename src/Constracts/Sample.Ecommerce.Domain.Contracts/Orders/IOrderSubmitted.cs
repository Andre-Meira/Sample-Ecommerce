using MassTransit;
using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Domain.Contracts.Orders;

[EntityName(nameof(IOrderSubmitted))]
public interface IOrderSubmitted
{
    public Guid Id { get; set; }
    public Guid IdClient { get; set; }
    public Guid IdProduct { get; set; }
    public DateTime Date { get; set; }

    public Address DeliveryAddress { get; set; } 
    public BankAccount BankAccount { get; set; }    
    public decimal Amount { get; set; }
    public decimal Value { get; set; }
}
