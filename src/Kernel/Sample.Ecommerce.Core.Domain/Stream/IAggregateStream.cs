namespace Sample.Ecommerce.Core.Domain.Stream;

public interface IAggregateStream<EventStream> where EventStream : IEventData
{
    void When(EventStream @event);
}
