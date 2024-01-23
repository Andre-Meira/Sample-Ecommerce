using Marten;
using Sample.Ecommerce.Order.Core.Orders.EventStream;

namespace Sample.Ecommerce.Order.Infra.Orders;

internal sealed class OrderStreamRepository : IOrderStreamRepository
{
    private readonly IDocumentSession _documentSession;

    public OrderStreamRepository(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public IEnumerable<IOrderStream> GetEvents(Guid IdOrder)
    {
        return _documentSession.Events.FetchStream(IdOrder).Select(e => (IOrderStream)e.Data);
    }

    public Task IncressEvent(IOrderStream @event)
    {
        var stream = _documentSession.Events.FetchStreamState(@event.IdCorrelation);

        if (stream is null)
        {
            _documentSession.Events.StartStream<Core.Orders.Order>(@event.IdCorrelation, @event);
            return _documentSession.SaveChangesAsync();
        }

        _documentSession.Events.Append(stream.Id, @event);
        return _documentSession.SaveChangesAsync();
    }
}
