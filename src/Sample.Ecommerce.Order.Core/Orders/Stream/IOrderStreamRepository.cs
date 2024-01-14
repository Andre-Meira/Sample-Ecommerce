namespace Sample.Ecommerce.Order.Core.Orders.EventStream;

public interface IOrderStreamRepository
{
    public IEnumerable<IOrderStream> GetEvents(Guid IdOrder);

    public Task IncressEvent(IOrderStream @event);
}
