using MassTransit;

namespace Sample.Ecommerce.Domain.Contracts.Orders;

[EntityName(nameof(SubmitOrder))]
public sealed record SubmitOrder : IContract 
{
    public Guid Id { get; set; }    
    public Guid IdClient { get; set; }
    public Guid IdProduct { get; set; }

    public DateTime Date { get; set; }    
    public decimal Quantity { get; set; }
    public decimal Value { get; set; }
}
