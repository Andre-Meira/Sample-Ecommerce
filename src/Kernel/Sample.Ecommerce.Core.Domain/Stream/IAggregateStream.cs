namespace Sample.Ecommerce.Core.Domain.Stream;

public interface IAggregateStream<EventStream> where EventStream : IEventStream
{
    void When(EventStream @event);
}
