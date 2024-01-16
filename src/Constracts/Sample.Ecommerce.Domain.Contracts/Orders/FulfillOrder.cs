using MassTransit;
using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Domain.Contracts.Orders;

[EntityName(nameof(SubmitOrder))]
public record FulfillOrder : IContract
{
    public FulfillOrder(Guid id, BaseAddress deliveryAddress, BaseBankAccount bankAccount, decimal amount, decimal value)
    {
        Id = id;
        DeliveryAddress = deliveryAddress;
        BankAccount = bankAccount;
        Amount = amount;
        Value = value;
    }

    public Guid Id { get; set; }

    public BaseAddress DeliveryAddress { get; set; }
    public BaseBankAccount BankAccount { get; set; }

    public decimal Amount { get; set; }
    public decimal Value { get; set; }
}
