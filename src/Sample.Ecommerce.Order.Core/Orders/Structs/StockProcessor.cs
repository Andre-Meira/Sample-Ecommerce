using Sample.Ecommerce.Core.Domain;
using Sample.Ecommerce.Domain.Contracts.Orders;
using Sample.Ecommerce.Order.Core.Inventorys;
using Sample.Ecommerce.Order.Core.Inventorys.EventStream;

namespace Sample.Ecommerce.Order.Core.Orders.Structs;

internal sealed class StockProcessor : OrderStructProcessor
{
    private readonly IOrderStructProcessor _processor;
    private readonly IStockRepository _stock;        

    public StockProcessor(IOrderStructProcessor processor, 
        IStockRepository stock) : base(processor)
    {
        _processor = processor;
        _stock = stock;
    }

    public override async Task Process(SubmitOrder order)
    {
        Inventory inventory = await _stock.Process(order.IdProduct);

        if (inventory.Amount < order.Amount)
            throw new DomainException("Não existe essa quantidade em estoque.");

        await _processor.Process(order);
    }
}
