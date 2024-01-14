using Sample.Ecommerce.Core.Domain.Stream;

namespace Sample.Ecommerce.Order.Core.Inventorys.EventStream;

public interface IStockProcessorEvents
: IProcessorEventStream<Inventory, IInventoryStream>
{ }

public sealed class InventoryStreamProcessor : IStockProcessorEvents
{
    private readonly IInventoryStreamRepository _streamRepositore;

    public InventoryStreamProcessor(IInventoryStreamRepository streamRespositore)
    {
        _streamRepositore = streamRespositore;
    }

    public IEnumerable<IInventoryStream> GetEvents(Guid Id)
    {
        return _streamRepositore.GetEvents(Id);
    }

    public async Task Include(IInventoryStream @event)
    {
        Inventory stream = await Process(@event.IdCorrelation);
        stream.When(@event);

        await _streamRepositore.IncressEvent(@event).ConfigureAwait(false);
    }

    public Task<Inventory> Process(Guid Id)
    {
        IEnumerable<IInventoryStream> events = GetEvents(Id);
        Inventory paymentEvent = new Inventory();

        foreach (IInventoryStream @event in events) paymentEvent.When(@event);

        return Task.FromResult(paymentEvent);
    }
}
