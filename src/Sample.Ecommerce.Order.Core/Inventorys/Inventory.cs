using Sample.Ecommerce.Core.Domain.Enums;
using Sample.Ecommerce.Core.Domain.Stream;
using Sample.Ecommerce.Order.Core.Inventorys.EventStream;
using Sample.Ecommerce.Order.Core.Products;

namespace Sample.Ecommerce.Order.Core.Inventorys;

public class Inventory : IAggregateStream<IInventoryStream>
{
    public Guid Id { get; set; }    

    public int Amount { get; set; }
    public decimal Value { get; set; }      
    public Product Product { get; set; } = null!;

    public Status Status { get; set; }
    
    public DateTime Created { get; set; }
    public DateTime Updated { get; set;}

    public void When(IInventoryStream @event) => @event.Process(this);     
}
