using Sample.Ecommerce.Core.Domain.ValueObjects;
using Sample.Ecommerce.Domain.Contracts.Orders;
using Sample.Ecommerce.Order.Core.Orders.EventStream;
using Sample.Ecommerce.Order.Core.Products;

namespace Sample.Ecommerce.Order.Core.Orders.Events;

internal sealed class OrderSubmitted : IOrderSubmitted, IOrderStream
{
    public OrderSubmitted(
        Guid id,
        Guid idClient,          
        BaseProduct product,
        BaseAddress deliveryAddress, 
        BaseBankAccount bankAccount,         
        int quantity)
    {        
        IdClient = idClient;        
        DeliveryAddress = deliveryAddress;
        BankAccount = bankAccount;
        Amount = quantity;        

        Id = id;
        Date = DateTime.UtcNow;        
        Product = product;

        IdCorrelation = Id;        
    }

    public Guid Id { get ; init; }
    public Guid IdCorrelation { get; init; }
    public Guid IdProduct { get; init; }

    public DateTime Date { get; init; }    
    public Guid IdClient { get ; init; }       

    public BaseProduct Product { get; init; }
    public BaseAddress DeliveryAddress { get ; init ; }
    public BaseBankAccount BankAccount { get ; init ; }

    public int Amount { get ; init; }  

    public void Process(Order order)
    {
        order.Status = StatusOrder.Submit;
        order.Date = Date;
        order.Id = Id;
        order.IdClient = IdClient;
        order.DeliveryAddress = DeliveryAddress;
        order.BankAccount = BankAccount;
        order.Amount = Amount; 
        order.Product = Product;
    }
}
