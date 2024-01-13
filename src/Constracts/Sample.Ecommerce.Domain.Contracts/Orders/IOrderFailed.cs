using MassTransit;

namespace Sample.Ecommerce.Domain.Contracts.Orders;

[EntityName(nameof(IOrderFailed))]
public interface IOrderFailed 
{
    public Guid  Id { get; set; }
    public string Message { get; set; } 
}
