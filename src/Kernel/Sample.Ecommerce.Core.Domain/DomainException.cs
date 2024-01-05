using Sample.Ecommerce.Core.Domain.Entity;

namespace Sample.Ecommerce.Core.Domain;

public class DomainException : Exception
{
    public List<Notification>? Messages { get; }

    public DomainException() { }

    public DomainException(string message) : base(message) { }

    public DomainException(string message, Exception innerException)
        : base(message, innerException) { }

    public DomainException(List<Notification> messages) : base() => Messages = messages;
}