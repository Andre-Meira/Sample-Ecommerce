namespace Sample.Ecommerce.Core.Domain.Entity;

public interface IEventNotificationDomain
{
    void RaiseDomainEvent(INotificationDomain @event);

    void ClearEvents();

    IReadOnlyCollection<INotificationDomain> GetEvents();
}

public interface INotificationDomain 
{
    public Guid Id { get; }
}