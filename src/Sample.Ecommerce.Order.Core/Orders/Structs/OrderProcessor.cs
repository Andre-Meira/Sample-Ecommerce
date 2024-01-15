using Sample.Ecommerce.Domain.Contracts.Orders;

namespace Sample.Ecommerce.Order.Core.Orders.Structs;

internal sealed class OrderProcessor : IOrderStructProcessor
{    
    public Task Process(SubmitOrder order)
    {
        throw new NotImplementedException();
    }
}
