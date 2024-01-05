using Sample.Ecommerce.Core.Domain.Stream;

namespace Sample.Ecommerce.Order.Core.Orders;

public class Order : IAggregateStream<IOrderProcessStream>
{
    public Guid Id { get; set; }
    public Guid IdClient { get ; set; }
    public Guid IdProduct { get; set; }

    public DateTime Date { get; set; }
    public StatusOrder Status { get; set; }

    public decimal Quantity { get; set; }  
    public decimal Value { get ; set; } 

    public void When(IOrderProcessStream @event) => @event.Process(this);    
}


public enum StatusOrder
{
    Process,
    Complet,
    Error,
    Reverse
}
