namespace Sample.Ecommerce.Core.Domain.Stream;

public class EventStream 
{
    public EventStream(IEventData @event)
    {
        IdCorrelation = @event.IdCorrelation;
        Name = @event.GetType().Name;
        Created = DateTime.UtcNow;
    }


    public static EventStream Initialized(IEventData @event) 
        => new EventStream(@event);

    public virtual Guid IdCorrelation  { get; init; }
    public virtual string Name { get; init; }   
    public virtual DateTime Created { get; init; }  
}
