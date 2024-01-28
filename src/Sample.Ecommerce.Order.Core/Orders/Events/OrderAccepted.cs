using Sample.Ecommerce.Domain.Contracts.Orders;
using Sample.Ecommerce.Order.Core.Orders.EventStream;

namespace Sample.Ecommerce.Order.Core.Orders.Events;

public record OrderAccepted : IOrderAccepted, IOrderStream
{
    public Guid Id { get; init; }
    public Guid IdCorrelation { get; init; }
    public StatusOrder Status { get; init; }

    public OrderAccepted(Guid id)
    {
        Id = id;
        IdCorrelation = id;
        Status = StatusOrder.Accept;
    }

    public void Process(Order order) => order.Status = Status;

}
