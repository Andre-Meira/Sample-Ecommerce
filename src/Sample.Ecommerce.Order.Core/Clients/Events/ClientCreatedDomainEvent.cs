using Sample.Ecommerce.Core.Domain.Entity;

namespace Sample.Ecommerce.Order.Core.Clients;

internal record ClientCreatedDomainEvent : INotificationDomain
{
    public ClientCreatedDomainEvent(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public Guid Id => Guid.NewGuid();

    public string Name { get; init; }

    public string Email { get; init; }      
}
