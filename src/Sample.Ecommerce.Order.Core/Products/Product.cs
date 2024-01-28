using Sample.Ecommerce.Core.Domain.Entity;
using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Order.Core.Products;

public record Product : BaseProduct, IAggregate
{   
    public Product(string name, decimal price, string? description) 
        : base(name, price, description) { }   
}
