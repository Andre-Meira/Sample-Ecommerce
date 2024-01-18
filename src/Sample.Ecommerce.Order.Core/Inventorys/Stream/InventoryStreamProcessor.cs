using Sample.Ecommerce.Core.Domain.Stream;

namespace Sample.Ecommerce.Order.Core.Inventorys.EventStream;

public interface IStockRepository
: IProcessorEventStream<Inventory, IInventoryStream>
{
    public Task<Inventory> GetByIdProduct(Guid Id);
}

public sealed class InventoryStreamProcessor : IStockRepository
{
    private readonly IInventoryStreamRepository _streamRepositore;

    public InventoryStreamProcessor(IInventoryStreamRepository streamRespositore)
    {
        _streamRepositore = streamRespositore;
    }

    public Task<Inventory> GetByIdProduct(Guid Id)
    {
        IEnumerable<IInventoryStream> events = _streamRepositore.GetEventsByFilter(e => e.IdProduct = Id);
        Inventory paymentEvent = new Inventory();

        foreach (IInventoryStream @event in events) paymentEvent.When(@event);

        return Task.FromResult(paymentEvent);
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
