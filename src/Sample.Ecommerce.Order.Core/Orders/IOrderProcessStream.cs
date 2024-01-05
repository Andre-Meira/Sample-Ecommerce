using Sample.Ecommerce.Core.Domain.Stream;

namespace Sample.Ecommerce.Order.Core.Orders;

public interface IOrderProcessStream : IEventStream
{
    public void Process(Order order);
}
