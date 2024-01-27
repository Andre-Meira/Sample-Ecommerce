namespace Sample.Ecommerce.Core.API.Observability;

public record ResultController(string Mensagem, int StatusCode, object? Data);
