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

    public BaseAddress DeliveryAddress { get; set; } 
    public BaseBankAccount BankAccount { get; set; }    
    public int Amount { get; set; }
    public decimal Value { get; set; }
}
