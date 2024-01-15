using Sample.Ecommerce.Domain.Contracts.Orders;

namespace Sample.Ecommerce.Order.Core.Orders.Structs;

internal sealed class BankAccountProcessor : OrderStructProcessor
{
    private readonly IOrderStructProcessor _processor;

    public BankAccountProcessor(IOrderStructProcessor processor) 
        : base(processor)
    {
        _processor = processor;
    }

    public override Task Process(SubmitOrder order)
    {
        return base.Process(order);
    }
}
