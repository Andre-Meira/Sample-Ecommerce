using Marten.Events.Projections;
using Marten.Events;
using Marten;
using Sample.Ecommerce.Order.Core.Inventorys.EventStream;

namespace Sample.Ecommerce.Order.Infra.Inventory;

public class InventoryProjectionHandler : IProjection
{
    public void Apply(IDocumentOperations operations, IReadOnlyList<StreamAction> streams)
    {
        IEnumerable<IInventoryStream> streamInventory = streams.SelectMany(x => x.Events)
            .OrderBy(s => s.Sequence)
            .Where(e => e.Data is IInventoryStream)
            .Select(e => (IInventoryStream)e.Data); 
            
        foreach (IInventoryStream @event in streams)
        {
            
        }
    }

    public Task ApplyAsync(IDocumentOperations operations, IReadOnlyList<StreamAction> streams, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }
}
