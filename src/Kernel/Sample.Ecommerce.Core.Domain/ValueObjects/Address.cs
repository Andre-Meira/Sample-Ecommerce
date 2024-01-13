namespace Sample.Ecommerce.Core.Domain.ValueObjects;

public record Address
{
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Complement { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; } 
    public string State { get; private set; }
    public string ZipCode { get; private set; }

    public Address(
        string street, string number, string complement, 
        string neighborhood, string city, string state, string zipCode)
    {
        Street = street ?? throw new ArgumentNullException(nameof(street));
        Number = number ?? throw new ArgumentNullException(nameof(number));
        Complement = complement ?? string.Empty;
        Neighborhood = neighborhood ?? throw new ArgumentNullException(nameof(neighborhood));
        City = city ?? throw new ArgumentNullException(nameof(city));
        State = state ?? throw new ArgumentNullException(nameof(state));
        ZipCode = zipCode ?? throw new ArgumentNullException(nameof(zipCode));
    }

#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    protected Address()
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    {

    }

}
