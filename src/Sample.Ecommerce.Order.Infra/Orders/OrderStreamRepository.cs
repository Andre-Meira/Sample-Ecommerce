using Sample.Ecommerce.Order.Core.Orders.EventStream;

namespace Sample.Ecommerce.Order.Infra.Orders;

internal sealed class OrderStreamRepository : IOrderStreamRepository
{
    public IEnumerable<IOrderStream> GetEvents(Guid IdOrder)
    {
        throw new NotImplementedException();
    }

    public Task IncressEvent(IOrderStream @event)
    {
        throw new NotImplementedException();
    }
}
