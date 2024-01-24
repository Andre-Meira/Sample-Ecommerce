using Sample.Ecommerce.Core.Domain.Entity;

namespace Sample.Ecommerce.Order.Core.Products;

public class Product : Entity, IAggregate
{
    public Product(string name, decimal price, string? description)
    {
        Name = name;
        Price = price;
        Description = description;
    }

    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string? Description { get; private set; }    
}
