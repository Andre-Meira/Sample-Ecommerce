using Sample.Ecommerce.Core.Domain.Entity;

namespace Sample.Ecommerce.Core.Domain;

public class DomainException : Exception
{
    public List<string>? Messages { get; }

    public DomainException() { }

    public DomainException(string message) : base(message) { }

    public DomainException(string message, Exception innerException)
        : base(message, innerException) { }

    public DomainException(List<string> messages) : base() => Messages = messages;
}