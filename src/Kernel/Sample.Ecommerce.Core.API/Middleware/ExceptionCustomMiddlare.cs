using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Ecommerce.Core.API.Observability;
using Sample.Ecommerce.Core.Domain;

namespace Sample.Ecommerce.Core.API.Middleware;

public class ErrorHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception err)
        {
            await HandlerExectionFilterAsync(context, err);
        }
    }

    public async Task HandlerExectionFilterAsync(HttpContext context, Exception exception)
    {
        if (exception is DomainException)
        {
            DomainException domainExceptions = (DomainException)exception;
            object data = domainExceptions.Message;

            if (domainExceptions.Messages?.Count > 0)
                data = domainExceptions.Messages;

            ResultController resultController = new ResultController("Error", 400, data);
            await context.Response.WriteAsJsonAsync(resultController);
        }

        if (exception is not  DomainException)
        {
            _logger.LogError(exception, "Request error {0}", exception.Message);

            ResultController resultController = new ResultController("Error", 500, exception.Message);
            ObjectResult badRequest = new ObjectResult(resultController) { StatusCode = 500 };
            await context.Response.WriteAsJsonAsync(resultController);
        }
    }
}
