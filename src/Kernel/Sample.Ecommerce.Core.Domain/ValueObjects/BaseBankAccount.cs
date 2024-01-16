namespace Sample.Ecommerce.Core.Domain.ValueObjects;

public record BaseBankAccount
{
    public string AccountNumber { get; private set; }
    public string AgencyNumber { get; private set; }
    public string AccountHolderName { get; private set; }
    public string BankName { get; private set; }

    public BaseBankAccount(string accountNumber, string agencyNumber, string accountHolderName, string bankName)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
        {
            throw new ArgumentException("Account number cannot be empty or null", nameof(accountNumber));
        }

        if (string.IsNullOrWhiteSpace(agencyNumber))
        {
            throw new ArgumentException("Agency number cannot be empty or null", nameof(agencyNumber));
        }

        if (string.IsNullOrWhiteSpace(accountHolderName))
        {
            throw new ArgumentException("Account holder name cannot be empty or null", nameof(accountHolderName));
        }

        if (string.IsNullOrWhiteSpace(bankName))
        {
            throw new ArgumentException("Bank name cannot be empty or null", nameof(bankName));
        }

        AccountNumber = accountNumber;
        AgencyNumber = agencyNumber;
        AccountHolderName = accountHolderName;
        BankName = bankName;
    }

#pragma warning disable CS8618 
    protected BaseBankAccount()
#pragma warning restore CS8618
    {

    }

    public override string ToString()
    {
        return $"Account: {AccountNumber}, Agency: {AgencyNumber}, Holder: {AccountHolderName}, Bank: {BankName}";
    }
}