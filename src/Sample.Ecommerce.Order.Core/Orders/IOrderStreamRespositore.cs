namespace Sample.Ecommerce.Order.Core.Orders;

public interface IOrderStreamRespositore
{
    public IEnumerable<IOrderProcessStream> GetEvents(Guid idPayment);

    public Task IncressEvent(IOrderProcessStream @event);
}
