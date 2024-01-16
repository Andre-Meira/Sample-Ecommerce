using Sample.Ecommerce.Core.Domain;
using Sample.Ecommerce.Core.Domain.Entity;
using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Order.Core.Clients;

public class Client : Entity, IAggregate
{
    private readonly List<Address> _addresses;
    private readonly List<BankAccount> _bankAccounts;

    public string Name { get; private set; }    
    public Cpf Cpf { get; private set; }
    public Email Email { get; private set; }

    public Client(string name, string cpf, string email)
    {
        Name = name;
        Cpf = new Cpf(cpf);
        Email = new Email(email);

        _addresses = new List<Address>();
        _bankAccounts = new List<BankAccount>();   
    }

    #pragma warning disable CS8618 
    protected Client() { }
    #pragma warning restore CS8618 

    public IReadOnlyCollection<Address> Addresses => _addresses;
    public IReadOnlyCollection<BankAccount> BankAccounts => _bankAccounts;

    public void RaiseAddress(BaseAddress address)
    {
        if (_addresses.Contains(address) == true)
            throw new DomainException("O enderoço Cadastrado já exite.");

        Address clientAddress = new Address(address);
        _addresses.Add(clientAddress);
    }

    public void RaiseBankAccount(BaseBankAccount bankAccount)
    {
        if (_bankAccounts.Contains(bankAccount) == true)
            throw new DomainException("A conta bancaria já existe.");

        BankAccount clientBankAccount = new BankAccount(bankAccount);
        _bankAccounts.Add(clientBankAccount);        
    }

    public override void Create()
    {
        RaiseDomainEvent(new ClientCreatedDomainEvent(Name, Email.Value));
        base.Create();         
    }
}
