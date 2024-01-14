using Sample.Ecommerce.Core.Domain.Stream;

namespace Sample.Ecommerce.Order.Core.Inventorys.EventStream;

public interface IInventoryStream : IEventData
{
    public void Process(Inventory stock);
}
