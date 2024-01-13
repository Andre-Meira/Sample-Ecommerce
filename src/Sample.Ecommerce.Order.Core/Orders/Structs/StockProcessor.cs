namespace Sample.Ecommerce.Order.Core.Orders.Structs;

internal sealed class StockProcessor : OrderStructProcessor
{
    private readonly IOrderStructProcessor _processor;    

    public StockProcessor(IOrderStructProcessor processor) : base(processor)
    {
        _processor = processor;
    }

    public override void Process(Order order)
    {
        _processor.Process(order);
    }
}
