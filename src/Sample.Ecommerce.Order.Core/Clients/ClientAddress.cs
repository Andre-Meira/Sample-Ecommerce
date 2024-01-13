using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Order.Core.Clients;

public record ClientAddress : Address
{
    public Guid Id { get; private set; }
    public Guid IdClient { get; private set; }

    public ClientAddress(Address address): base(address)
    {
        IdClient = Guid.Empty; 
        Id = Guid.NewGuid();
    }        

    private ClientAddress() 
    {
        IdClient = Guid.Empty;
        Id = Guid.NewGuid();
    }
}
