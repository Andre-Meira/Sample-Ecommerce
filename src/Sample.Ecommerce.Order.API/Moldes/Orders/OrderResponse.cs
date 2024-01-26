namespace Sample.Ecommerce.Order.API.Moldes.Orders;

public record OrderResponse(Guid IdOrder, string? Message = null);
public record OrderResponse<T>(Guid IdOrder, T Data);
public record OrderStatus(string status);