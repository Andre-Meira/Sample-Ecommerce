namespace Sample.Ecommerce.Order.API.Moldes;

public record OrderCommand 
{
    public Guid IdClient { get; set; }
    public Guid IdProduct { get; set; }

    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public decimal Value { get; set; }
}
