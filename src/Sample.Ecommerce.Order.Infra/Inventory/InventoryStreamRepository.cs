using Marten;
using Microsoft.Extensions.Logging;
using Sample.Ecommerce.Order.Core.Inventorys.EventStream;

namespace Sample.Ecommerce.Order.Infra.Inventory;

internal sealed class InventoryStreamRepository : IInventoryStreamRepository
{
    private readonly IDocumentSession _documentSession;
    private readonly ILogger<IInventoryStreamRepository> _logger;

    public InventoryStreamRepository(IDocumentSession documentSession, ILogger<IInventoryStreamRepository> logger)
    {
        _documentSession = documentSession;
        _logger = logger;
    }

    public IEnumerable<IInventoryStream> GetEvents(Guid idInventory)
    {
        return _documentSession.Events.FetchStream(idInventory).Select(e => (IInventoryStream)e.Data);
    }    

    public async Task IncressEvent(IInventoryStream @event)
    {
        _logger.LogInformation("Incress event {0}, id:{1}", nameof(@event), @event.IdCorrelation);

        var stream = _documentSession.Events.FetchStreamState(@event.IdCorrelation);

        if (stream is null)
        {
            _documentSession.Events.StartStream<Core.Inventorys.Inventory>(@event.IdCorrelation);            
        }

        _documentSession.Events.Append(@event.IdCorrelation, @event);
        await _documentSession.SaveChangesAsync().ConfigureAwait(false);

        _logger.LogInformation("id:{0} incress sucessing.", @event.IdCorrelation);
    }
}
