using MassTransit;

namespace Sample.Ecommerce.Domain.Contracts.Orders;

[EntityName(nameof(IOrderFulfillmentCompleted))]
public interface IOrderFulfillmentCompleted
{
    public  Guid Id { get; set; }
}
