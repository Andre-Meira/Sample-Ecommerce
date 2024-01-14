using MediatR;
using Sample.Ecommerce.Core.Domain.ValueObjects;
using Sample.Ecommerce.Order.Core.Products;

namespace Sample.Ecommerce.Order.Core.Orders.Commands;

internal sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    public Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}


public class CreateOrderCommand : IRequest<Guid>
{
    public Guid IdClient { get; set; }
    public DateTime Date { get; set; }
    public StatusOrder Status { get; set; }

    public Address DeliveryAddress { get; set; } = null!;
    public BankAccount BankAccount { get; set; } = null!;
    public Product Product { get; set; } = null!;

    public decimal Amount { get; set; }
    public decimal Value { get; set; }
}