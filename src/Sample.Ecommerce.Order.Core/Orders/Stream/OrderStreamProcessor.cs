using Sample.Ecommerce.Core.Domain.Stream;

namespace Sample.Ecommerce.Order.Core.Orders.EventStream;

public interface IOrderProcessorEvents
: IProcessorEventStream<Order, IOrderStream>
{ }

public sealed class OrderStreamProcessor : IOrderProcessorEvents
{
    private readonly IOrderStreamRepository _streamRepository;

    public OrderStreamProcessor(IOrderStreamRepository streamRespositore)
    {
        _streamRepository = streamRespositore;
    }

    public IEnumerable<IOrderStream> GetEvents(Guid Id)
    {
        return _streamRepository.GetEvents(Id);
    }

    public async Task Include(IOrderStream @event, CancellationToken cancellationToken = default)
    {
        Order stream = await Process(@event.IdCorrelation, cancellationToken);
        stream.When(@event);

        await _streamRepository.IncressEvent(@event).ConfigureAwait(false);
    }

    public Task<Order> Process(Guid Id, CancellationToken cancellationToken = default)
    {
        IEnumerable<IOrderStream> events = GetEvents(Id);
        Order paymentEvent = new Order();

        foreach (IOrderStream @event in events) paymentEvent.When(@event);

        return Task.FromResult(paymentEvent);
    }
}
