using MassTransit;
using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Domain.Contracts.Orders;

[EntityName(nameof(SubmitOrder))]
public record SubmitOrder : IContract 
{
    public SubmitOrder(Guid id, Guid idClient, Guid idProduct, 
        DateTime date, BaseAddress deliveryAddress, BaseBankAccount bankAccount, 
        int amount, decimal value)
    {
        Id = id;
        IdClient = idClient;
        IdProduct = idProduct;
        Date = date;
        DeliveryAddress = deliveryAddress;
        BankAccount = bankAccount;
        Amount = amount;
        Value = value;
    }

    public Guid Id { get; set; }
    public Guid IdClient { get; set; }
    public Guid IdProduct { get; set; }
    public DateTime Date { get; set; }

    public BaseAddress DeliveryAddress { get; set; }
    public BaseBankAccount BankAccount { get; set; }

    public int Amount { get; set; }
    public decimal Value { get; set; }
}
