using MassTransit;

namespace Sample.Ecommerce.Domain.Contracts.Orders;

[EntityName(nameof(IOrderAccepted))]
public interface IOrderAccepted
{
    public  Guid Id { get; init; }
}
