namespace Sample.Ecommerce.Core.Domain.Stream;


/// <summary>
///     Clase base de eventos, todos os eventos do 
///     sistema devera abstrair dessa interface
/// </summary>
public interface IEventData
{
    public Guid IdCorrelation { get; init; }       
}
