using MassTransit;

namespace Sample.Ecommerce.Domain.Contracts.Orders;

[EntityName(nameof(IOrderFulfillmentFaulted))]
public interface IOrderFulfillmentFaulted
{
    public Guid Id { get; set; }
    public string Message{ get; set; }
}
