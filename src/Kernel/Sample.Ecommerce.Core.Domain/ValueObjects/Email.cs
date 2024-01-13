using Sample.Ecommerce.Core.Domain;

namespace Sample.Ecommerce.Core.Domain.ValueObjects;

public record Email
{
    public string Value { get; private set; }

    public Email(string email)
    {
        if (!IsValidEmail(email))
        {
            throw new DomainException("Invalid email address");
        }

        Value = email;
    }

#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    private Email()
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    {
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var mailAddress = new System.Net.Mail.MailAddress(email);
            return mailAddress.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
