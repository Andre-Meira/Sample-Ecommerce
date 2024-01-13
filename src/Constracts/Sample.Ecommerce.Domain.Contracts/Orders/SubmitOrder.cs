using MassTransit;

namespace Sample.Ecommerce.Domain.Contracts.Orders;

[EntityName(nameof(SubmitOrder))]
public record SubmitOrder : IContract 
{
    public Guid Id { get; set; }    
    public Guid IdClient { get; set; }
    public Guid IdProduct { get; set; }

    public DateTime Date { get; set; }    
    public decimal Amount { get; set; }
    public decimal Value { get; set; }
}
