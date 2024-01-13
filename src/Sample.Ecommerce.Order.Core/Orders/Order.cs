using Sample.Ecommerce.Core.Domain.ValueObjects;
using Sample.Ecommerce.Core.Domain.Stream;
using Sample.Ecommerce.Order.Core.Orders.EventStream;

namespace Sample.Ecommerce.Order.Core.Orders;

public enum StatusOrder { Process, Complet, Error, Reverse }

public class Order : IAggregateStream<IOrderStream>
{
    public Guid Id { get; set; }
    public Guid IdClient { get ; set; }   
    public DateTime Date { get; set; }
    public StatusOrder Status { get; set; }

    public Address DeliveryAddress { get; set; } = null!;
    public BankAccount BankAccount { get; set; } = null!;

    public decimal Amount { get; set; }  
    public decimal Value { get ; set; } 

    public void When(IOrderStream @event) => @event.Process(this);    
}
