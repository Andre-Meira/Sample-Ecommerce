using Sample.Ecommerce.Core.Domain.Stream;

namespace Sample.Ecommerce.Order.Core.Orders;

public interface IOrderProcessorEvents
: IProcessorEventStream<Order, IOrderProcessStream> { }

public sealed class OrderProcessorEvents : IOrderProcessorEvents
{
    private readonly IOrderStreamRespositore _streamRespositore;

    public OrderProcessorEvents(IOrderStreamRespositore streamRespositore)
    {
        _streamRespositore = streamRespositore;
    }

    public IEnumerable<IOrderProcessStream> GetEvents(Guid Id)
    {
        return _streamRespositore.GetEvents(Id);
    }

    public async Task Include(IOrderProcessStream @event)
    {
        Order stream = await Process(@event.IdCorrelation);
        stream.When(@event);

        await _streamRespositore.IncressEvent(@event).ConfigureAwait(false);
    }

    public Task<Order> Process(Guid Id)
    {
        IEnumerable<IOrderProcessStream> events = GetEvents(Id);
        Order paymentEvent = new Order();

        foreach (IOrderProcessStream @event in events) paymentEvent.When(@event);

        return Task.FromResult(paymentEvent);
    }
}
