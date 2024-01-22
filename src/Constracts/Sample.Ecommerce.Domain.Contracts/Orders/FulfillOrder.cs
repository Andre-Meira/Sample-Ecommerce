using MassTransit;
using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Domain.Contracts.Orders;

[EntityName(nameof(SubmitOrder))]
public record FulfillOrder : IContract
{
    public FulfillOrder(Guid id, Guid idProduct, BaseAddress deliveryAddress, 
        BaseBankAccount bankAccount, int amount, decimal value)
    {
        Id = id;
        IdProduct = idProduct;
        DeliveryAddress = deliveryAddress;
        BankAccount = bankAccount;
        Amount = amount;
        Value = value;
    }

    public Guid Id { get; set; }

    public Guid IdProduct { get; set; }

    public BaseAddress DeliveryAddress { get; set; }
    public BaseBankAccount BankAccount { get; set; }

    public int Amount { get; set; }
    public decimal Value { get; set; }
}
