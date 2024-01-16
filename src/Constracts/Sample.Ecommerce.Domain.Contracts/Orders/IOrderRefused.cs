using MassTransit;

namespace Sample.Ecommerce.Domain.Contracts.Orders;

[EntityName(nameof(IOrderRefused))]
public interface IOrderRefused
{
    public Guid  Id { get; set; }
    public string Message { get; set; } 
}
