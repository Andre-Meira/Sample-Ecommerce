using MassTransit;
using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Domain.Contracts.Orders;

[EntityName(nameof(SubmitOrder))]
public record FulfillOrder : IContract
{
    public FulfillOrder(Guid id, BaseProduct product, BaseAddress deliveryAddress, 
        BaseBankAccount bankAccount, int amount)
    {
        Id = id;
        Product = product;
        DeliveryAddress = deliveryAddress;
        BankAccount = bankAccount;
        Amount = amount;        
    }

    public Guid Id { get; set; }

    public BaseProduct Product { get; set; }

    public BaseAddress DeliveryAddress { get; set; }
    public BaseBankAccount BankAccount { get; set; }

    public int Amount { get; set; }    
}
