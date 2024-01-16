using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Order.Core.Clients;

public record Address : BaseAddress
{
    public Guid Id { get; private set; }
    public Guid IdClient { get; private set; }

    public Address(BaseAddress address): base(address)
    {
        IdClient = Guid.Empty; 
        Id = Guid.NewGuid();
    }        

    private Address() 
    {
        IdClient = Guid.Empty;
        Id = Guid.NewGuid();
    }
}
