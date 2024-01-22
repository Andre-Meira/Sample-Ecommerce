using MassTransit;
using Microsoft.Extensions.Logging;
using Sample.Ecommerce.Core.Domain.ValueObjects;

namespace Sample.Ecommerce.Order.Core.Orders.Machine.Activity;

public record ProcessPaymentArgunments(Guid IdOrder, BaseBankAccount BankAccount);

public interface IProcessPaymentResponse { public Guid IdOrder { get; set; } }


internal sealed class ProcessPaymentActivity : IActivity<ProcessPaymentArgunments, IProcessPaymentResponse>
{
    private readonly ILogger<ProcessPaymentActivity> _logger;

    public static readonly Uri Endpoint = new Uri("exchange:process-payment_execute");
    public const string Name = nameof(ProcessPaymentActivity);

    public ProcessPaymentActivity(ILogger<ProcessPaymentActivity> logger)
    {
        _logger = logger;
    }

    public async Task<CompensationResult> Compensate(CompensateContext<IProcessPaymentResponse> context)
    {
        _logger.LogInformation("Compensate payment order {0}", context.Log.IdOrder);

        await Task.Delay(TimeSpan.FromSeconds(5));
        return context.Compensated();
    }

    public async Task<ExecutionResult> Execute(ExecuteContext<ProcessPaymentArgunments> context)
    {
        _logger.LogInformation("Process payment order {0}", context.Arguments.IdOrder);
        await Task.Delay(TimeSpan.FromSeconds(5));

        _logger.LogInformation("Process completed.");
        return context.Completed<IProcessPaymentResponse>(new {  context.Arguments.IdOrder });
    }
}


