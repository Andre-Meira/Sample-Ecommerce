using Sample.Ecommerce.Core.Domain;
using Sample.Ecommerce.Order.Core.Inventorys.EventStream;

namespace Sample.Ecommerce.Order.Core.Inventorys.Events;

public sealed record ProductSelled : IInventoryStream
{
    public ProductSelled(Guid idCorrelation, int amount)
    {
        IdCorrelation = idCorrelation;
        Amount = amount;
    }

    public Guid IdCorrelation { get; init; }
    public int Amount { get; init; }   

    public void Process(Inventory stock)
    {
        if (stock.Amount < Amount)
            throw new DomainException("Não é possivel realizar a venda, " +
                "sem quantidade necessaria.");

        stock.Amount -= Amount;
        stock.Updated = DateTime.UtcNow;
    }
}
