﻿namespace Sample.Ecommerce.Core.Domain.Stream;

public interface IProcessorEventStream<ProcessStream, EventStream>    
    where ProcessStream : IAggregateStream<EventStream>
    where EventStream : IEventData
{
    Task Include(EventStream @event);

    Task<ProcessStream> Process(Guid Id);

    IEnumerable<EventStream> GetEvents(Guid Id);
}
