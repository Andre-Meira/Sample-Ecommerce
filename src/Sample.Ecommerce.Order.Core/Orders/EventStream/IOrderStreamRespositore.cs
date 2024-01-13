namespace Sample.Ecommerce.Order.Core.Orders.EventStream;

public interface IOrderStreamRespositore
{
    public IEnumerable<IOrderStream> GetEvents(Guid idPayment);

    public Task IncressEvent(IOrderStream @event);
}
