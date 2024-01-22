using Sample.Ecommerce.Core.Domain.ValueObjects;
using Sample.Ecommerce.Domain.Contracts.Orders;
using Sample.Ecommerce.Order.Core.Orders.EventStream;

namespace Sample.Ecommerce.Order.Core.Orders.Events;

internal sealed class OrderSubmitted : IOrderSubmitted, IOrderStream
{
    public OrderSubmitted(
        Guid id,
        Guid idClient,          
        Guid idProduct,
        BaseAddress deliveryAddress, 
        BaseBankAccount bankAccount,         
        int quantity, decimal value)
    {        
        IdClient = idClient;        
        DeliveryAddress = deliveryAddress;
        BankAccount = bankAccount;
        Amount = quantity;
        Value = value;

        Id = id;
        Date = DateTime.UtcNow;
        IdProduct = idProduct;

        IdCorrelation = Id;        
    }

    public Guid Id { get ; set ; }
    public Guid IdCorrelation { get; init; }
    public Guid IdProduct { get; set ; }

    public DateTime Date { get; set; }    
    public Guid IdClient { get ; set ; }
    
    public BaseAddress DeliveryAddress { get ; set ; }
    public BaseBankAccount BankAccount { get ; set ; }

    public int Amount { get ; set ; }
    public decimal Value { get ; set ; }    

    public void Process(Order order)
    {
        order.Status = StatusOrder.Submit;
        order.Date = Date;
        order.Id = Id;
        order.IdClient = IdClient;
        order.DeliveryAddress = DeliveryAddress;
        order.BankAccount = BankAccount;
        order.Amount = Amount;
        order.IdProduct = IdProduct;
        order.Value = Value;        
    }
}
