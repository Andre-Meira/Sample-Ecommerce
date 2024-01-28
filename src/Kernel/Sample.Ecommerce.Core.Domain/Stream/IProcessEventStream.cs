namespace Sample.Ecommerce.Core.Domain.Stream;

public interface IProcessorEventStream<ProcessStream, EventStream>    
    where ProcessStream : IAggregateStream<EventStream>
    where EventStream : IEventData
{
    Task Include(EventStream @event, CancellationToken cancellationToken = default);

    Task<ProcessStream> Process(Guid Id, CancellationToken cancellationToken = default);

    IEnumerable<EventStream> GetEvents(Guid Id);
}
