using Sample.Ecommerce.Order.Core.Inventorys.EventStream;

namespace Sample.Ecommerce.Order.Core.Inventorys.Events;

public sealed record InventorynIcreased : IInventoryStream
{
    public Guid IdCorrelation { get; init; }
    public int Amount { get; init; }

    public InventorynIcreased(Guid idCorrelation, int amount)
    {
        IdCorrelation = idCorrelation;
        Amount = amount;
    }    

    public void Process(Inventory stock)
    {
        stock.Amount += Amount;
        stock.Updated = DateTime.UtcNow;    
    }
}
