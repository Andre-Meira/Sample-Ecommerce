using MassTransit;
using Sample.Ecommerce.Domain.Contracts.Orders;

namespace Sample.Ecommerce.Order.Core.Orders.Workers;

internal sealed class FulfillOrderConsumer : IConsumer<FulfillOrder>
{
    public Task Consume(ConsumeContext<FulfillOrder> context)
    {
        throw new NotImplementedException();
    }
}
