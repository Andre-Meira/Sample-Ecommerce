using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Order.Core.Clients;

public record BankAccount : BaseBankAccount
{
    public Guid Id { get; private set; }

    public Guid IdClient {  get; private set; } 
  
    public BankAccount(BaseBankAccount bankAccount) : base(bankAccount) 
    {
        IdClient = Guid.Empty;
        Id = Guid.NewGuid();
    }

    private BankAccount() 
    {
        IdClient = Guid.Empty;
        Id = Guid.NewGuid();
    }
}
