namespace Sample.Ecommerce.Order.API.Moldes.Inventorys;

public record InventoryCreateCommand
{
    public Guid IdProduct { get; set; }
    public int Amount { get; set; }
}
