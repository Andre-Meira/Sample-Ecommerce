using Sample.Ecommerce.Core.Domain.ValueObjects;
using Sample.Ecommerce.Core.Domain.Stream;
using Sample.Ecommerce.Order.Core.Orders.EventStream;
using Sample.Ecommerce.Order.Core.Products;

namespace Sample.Ecommerce.Order.Core.Orders;

public enum StatusOrder { Submit, Accept, Refuse, FulfilmentComplet, FulfilmentFault}

public class Order : IAggregateStream<IOrderStream>
{
    public Guid Id { get; set; }
    public Guid IdClient { get ; set; }          

    public DateTime Date { get; set; }
    public StatusOrder Status { get; set; }

    public BaseAddress DeliveryAddress { get; set; } = null!;
    public BaseBankAccount BankAccount { get; set; } = null!;
    public BaseProduct Product { get; set; } = null!;

    public decimal Amount { get; set; }
    public decimal Value => Amount * Product.Price;

    public void When(IOrderStream @event) => @event.Process(this);    
}
