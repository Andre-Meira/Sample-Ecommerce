using Sample.Ecommerce.Domain.Contracts.Orders;

namespace Sample.Ecommerce.Order.Core.Orders.Structs;

public interface IOrderStructProcessor
{
    Task Process(SubmitOrder order);
}

internal abstract class OrderStructProcessor : IOrderStructProcessor
{
    private readonly IOrderStructProcessor _processor;    

    protected OrderStructProcessor(IOrderStructProcessor processor) => _processor = processor;

    public virtual Task Process(SubmitOrder order) => _processor.Process(order);   
}