using Sample.Ecommerce.Core.Domain.Stream;
using Sample.Ecommerce.Order.Core.Inventorys;

namespace Sample.Ecommerce.Order.Core.Orders.EventStream;

public interface IOrderStream : IEventData
{
    public void Process(Order order);
}
