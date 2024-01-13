namespace Sample.Ecommerce.Order.Core.Orders.Structs;

internal sealed class BankAccountProcessor : OrderStructProcessor
{
    private readonly IOrderStructProcessor _processor;

    public BankAccountProcessor(IOrderStructProcessor processor) 
        : base(processor)
    {
        _processor = processor;
    }

    public override void Process(Order order)
    {
        base.Process(order);
    }
}
