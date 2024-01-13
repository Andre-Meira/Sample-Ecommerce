using Sample.Ecommerce.Core.Domain;
using Sample.Ecommerce.Core.Domain.Entity;
using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Order.Core.Clients;

public class Client : Entity, IAggregate
{
    private readonly List<ClientAddress> _addresses;
    private readonly List<ClientBankAccount> _bankAccounts;

    public string Name { get; private set; }    
    public Cpf Cpf { get; private set; }
    public Email Email { get; private set; }

    public Client(string name, string cpf, string email)
    {
        Name = name;
        Cpf = new Cpf(cpf);
        Email = new Email(email);

        _addresses = new List<ClientAddress>();
        _bankAccounts = new List<ClientBankAccount>();   
    }

    #pragma warning disable CS8618 
    protected Client() { }
    #pragma warning restore CS8618 

    public IReadOnlyCollection<ClientAddress> Addresses => _addresses;
    public IReadOnlyCollection<ClientBankAccount> BankAccounts => _bankAccounts;

    public void RaiseAddress(Address address)
    {
        if (_addresses.Contains(address) == true)
            throw new DomainException("O enderoço Cadastrado já exite.");

        ClientAddress clientAddress = new ClientAddress(address);
        _addresses.Add(clientAddress);
    }

    public void RaiseBankAccount(BankAccount bankAccount)
    {
        if (_bankAccounts.Contains(bankAccount) == true)
            throw new DomainException("A conta bancaria já existe.");

        ClientBankAccount clientBankAccount = new ClientBankAccount(bankAccount);
        _bankAccounts.Add(clientBankAccount);        
    }

    public override void Create()
    {
        RaiseDomainEvent(new ClientCreatedDomainEvent(Name, Email.Value));
        base.Create();         
    }
}
