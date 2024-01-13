using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Order.Core.Clients;

public record ClientBankAccount : BankAccount
{
    public Guid Id { get; private set; }

    public Guid IdClient {  get; private set; } 
  
    public ClientBankAccount(BankAccount bankAccount) : base(bankAccount) 
    {
        IdClient = Guid.Empty;
        Id = Guid.NewGuid();
    }

    private ClientBankAccount() 
    {
        IdClient = Guid.Empty;
        Id = Guid.NewGuid();
    }
}
