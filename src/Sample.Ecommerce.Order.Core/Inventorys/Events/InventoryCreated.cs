using Sample.Ecommerce.Core.Domain;
using Sample.Ecommerce.Core.Domain.Enums;
using Sample.Ecommerce.Order.Core.Inventorys.EventStream;
using Sample.Ecommerce.Order.Core.Products;

namespace Sample.Ecommerce.Order.Core.Inventorys.Events;

public sealed record InventoryCreated : IInventoryStream
{
    public InventoryCreated(Product product,int amount)
    {                
        Amount = amount;             
        Product = product;

        IdCorrelation = Product.Id;
        Status = Status.Enable;
        Created = DateTime.UtcNow;
    }

    public Guid IdCorrelation { get; init; }
    public Product Product { get; init; }
    
    public int Amount { get; init; }
    public decimal Value { get; init; }
    public Status Status { get; init; }
    public DateTime Created { get; init; }    

    public void Process(Inventory stock)
    {
        if (stock.Id.Equals(Guid.Empty) == false)
            throw new DomainException("O Inventario já está criado.");

        stock.Status = Status.Enable;
        stock.Created = DateTime.UtcNow;        
        stock.Amount = Amount;        
        stock.Product = Product;

        stock.Id = IdCorrelation;
    }
}
