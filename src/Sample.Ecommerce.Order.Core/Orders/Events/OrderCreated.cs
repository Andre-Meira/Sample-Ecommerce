using Sample.Ecommerce.Core.Domain.ValueObjects;
using Sample.Ecommerce.Domain.Contracts.Orders;
using Sample.Ecommerce.Order.Core.Orders.EventStream;
using Sample.Ecommerce.Order.Core.Products;

namespace Sample.Ecommerce.Order.Core.Orders.Events;

internal sealed class OrderCreated : IOrderCreated, IOrderStream
{
    public OrderCreated(
        Guid idClient,  
        Product product,
        Address deliveryAddress, 
        BankAccount bankAccount,         
        decimal quantity, decimal value)
    {
        IdClient = idClient;
        Product = product;
        DeliveryAddress = deliveryAddress;
        BankAccount = bankAccount;
        Amount = quantity;
        Value = value;

        Id = Guid.NewGuid();
        Date = DateTime.UtcNow;
        IdProduct = product.Id;

        IdCorrelation = Id;        
    }

    public Guid Id { get ; set ; }
    public Guid IdCorrelation { get; init; }
    public Guid IdProduct { get; set ; }

    public DateTime Date { get; set; }    
    public Guid IdClient { get ; set ; }
    
    public Address DeliveryAddress { get ; set ; }
    public BankAccount BankAccount { get ; set ; }

    public decimal Amount { get ; set ; }
    public decimal Value { get ; set ; }
    public Product Product { get; set ; }    

    public void Process(Order order)
    {
        order.Status = StatusOrder.Process;
        order.Date = Date;
        order.Id = Id;
        order.IdClient = IdClient;
        order.DeliveryAddress = DeliveryAddress;
        order.BankAccount = BankAccount;
        order.Amount = Amount;
        order.Value = Value;        
    }
}
