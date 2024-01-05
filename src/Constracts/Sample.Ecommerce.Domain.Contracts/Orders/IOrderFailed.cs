namespace Sample.Ecommerce.Domain.Contracts.Orders;

public interface IOrderFailed 
{
    public Guid  Id { get; set; }
    public string Message { get; set; } 
}
