using MassTransit;

namespace Sample.Ecommerce.Domain.Contracts.Orders;

[EntityName(nameof(IOrderCompleted))]
public interface IOrderCompleted
{
    public  Guid Id { get; set; }
}
