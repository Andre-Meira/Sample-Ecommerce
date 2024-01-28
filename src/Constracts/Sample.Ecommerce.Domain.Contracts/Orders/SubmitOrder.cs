using MassTransit;
using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Domain.Contracts.Orders;

[EntityName(nameof(SubmitOrder))]
public record SubmitOrder : IContract 
{
    public SubmitOrder(Guid idClient, Guid idProduct, 
        BaseAddress deliveryAddress, BaseBankAccount bankAccount, 
        int amount)
    {
        Id = Guid.NewGuid();
        IdClient = idClient;
        IdProduct = idProduct;
        Date = DateTime.Now;
        DeliveryAddress = deliveryAddress;
        BankAccount = bankAccount;
        Amount = amount;        
    }

    public Guid Id { get; set; }
    public Guid IdClient { get; set; }
    public Guid IdProduct { get; set; }
    public DateTime Date { get; set; }

    public BaseAddress DeliveryAddress { get; set; }
    public BaseBankAccount BankAccount { get; set; }

    public int Amount { get; set; }    
}
