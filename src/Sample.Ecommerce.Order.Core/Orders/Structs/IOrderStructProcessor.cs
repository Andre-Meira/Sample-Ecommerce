namespace Sample.Ecommerce.Order.Core.Orders.Structs;

public interface IOrderStructProcessor
{
    void Process(Order order);
}

internal abstract class OrderStructProcessor : IOrderStructProcessor
{
    private readonly IOrderStructProcessor _processor;    

    protected OrderStructProcessor(IOrderStructProcessor processor) => _processor = processor;

    public virtual void Process(Order order) => _processor.Process(order);   
}