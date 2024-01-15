namespace Sample.Ecommerce.Order.Core.Inventorys.EventStream;

public interface IInventoryStreamRepository
{
    public IEnumerable<IInventoryStream> GetEvents(Guid idInventory);

    public IEnumerable<IInventoryStream> GetEventsByFilter(Action<InventoryFilter> action);

    public Task IncressEvent(IInventoryStream @event);
}

public sealed record InventoryFilter
{
    public Guid? IdProduct { get; set; } = null;
};
