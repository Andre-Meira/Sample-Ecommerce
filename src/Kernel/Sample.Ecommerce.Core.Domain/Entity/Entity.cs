namespace Sample.Ecommerce.Core.Domain.Entity;

public class Entity : IEventNotificationDomain
{
    
    private Guid _id = Guid.Empty;

    private readonly List<INotificationDomain> _notificationDomains = new List<INotificationDomain>();

    public bool IsDeleted { get; private set; }

    public Guid Id { get; private set; }

    public virtual void Create()
    {
        Validate();

        if (_id.Equals(Guid.Empty) == false)
            throw new DomainException("já existe um id cadastrado para essa entidade.");

        _id = Guid.NewGuid();
        Id = _id;
    }

    public void ClearEvents()
    {
        _notificationDomains.Clear();
    }

    public IReadOnlyCollection<INotificationDomain> GetEvents()
    {
        return _notificationDomains;
    }

    public void RaiseDomainEvent(INotificationDomain @event)
    {
        _notificationDomains.Add(@event);
    }

    protected virtual void Validate() { }
}
