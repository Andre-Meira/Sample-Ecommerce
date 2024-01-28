namespace Sample.Ecommerce.Core.Domain.ValueObjects;

public record BaseProduct 
{
    public BaseProduct(string name, decimal price, string? description = null)
    {
        Name = name;
        Price = price;
        Description = description;

        Id = Guid.NewGuid();
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string? Description { get; private set; }
}
