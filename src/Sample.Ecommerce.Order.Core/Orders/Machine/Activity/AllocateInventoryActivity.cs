using MassTransit;
using Microsoft.Extensions.Logging;
using Sample.Ecommerce.Order.Core.Inventorys.Events;
using Sample.Ecommerce.Order.Core.Inventorys.EventStream;

namespace Sample.Ecommerce.Order.Core.Orders.Machine.Activity;

internal sealed class AllocateInventoryActivity : IActivity<AllocateInventoryArguments, IAllocateInventoryResponse>
{
    public static readonly Uri Endpoint = new Uri("exchange:bank-process_execute");
    
    private readonly ILogger<AllocateInventoryActivity> _logger;
    private readonly IStockRepository _stockRepository;    

    public AllocateInventoryActivity(
        ILogger<AllocateInventoryActivity> logger, 
        IStockRepository stockRepository)
    {
        _logger = logger;
        _stockRepository = stockRepository;
    }

    public async Task<CompensationResult> Compensate(CompensateContext<IAllocateInventoryResponse> context)
    {       
        var stock = await _stockRepository.GetByIdProduct(context.Log.IdProduct);

        ProductSellReversed productSelled = new ProductSellReversed(stock.Id, context.Log.Amount);
        stock.When(productSelled);

        _logger.LogInformation("Inventory deallocate, product:{0}, order:{1}",
            context.Log.IdProduct, context.Log.IdOrder);

        await _stockRepository.Include(productSelled).ConfigureAwait(false);

        return context.Compensated();
    }

    public async Task<ExecutionResult> Execute(ExecuteContext<AllocateInventoryArguments> context)
    {
        var stock = await _stockRepository.GetByIdProduct(context.Arguments.IdProduct);

        ProductSelled productSelled = new ProductSelled(stock.Id, context.Arguments.Amount);
        stock.When(productSelled);

        _logger.LogInformation("Inventory allocate, product:{0}, order:{1}, ammout:{2}",
            context.Arguments.IdProduct, context.Arguments.IdOrder, context.Arguments.Amount);

        await _stockRepository.Include(productSelled).ConfigureAwait(false);

        return context.Completed(new {context.Arguments.IdOrder, 
            context.Arguments.IdProduct,  context.Arguments.Amount});
    }
}

public record AllocateInventoryArguments(Guid IdOrder, Guid IdProduct, int Amount);

public interface IAllocateInventoryResponse
{ 
    Guid IdOrder { get; init; }    
    Guid IdProduct { get; init; }    
    int Amount { get; init; }   
}
