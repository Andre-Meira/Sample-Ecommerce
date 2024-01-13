using Sample.Ecommerce.Core.Domain.Stream;

namespace Sample.Ecommerce.Order.Core.Orders.EventStream;

public interface IOrderStream : IEventStream
{
    public void Process(Order order);
}
