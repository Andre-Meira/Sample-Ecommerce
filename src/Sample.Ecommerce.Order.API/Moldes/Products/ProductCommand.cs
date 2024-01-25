namespace Sample.Ecommerce.Order.API.Moldes;

public record ProductCommand 
{
    public ProductCommand(string name, decimal price, string? description)
    {
        Name = name;
        Price = price;
        Description = description;
    }

    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string? Description { get; private set; }
}
