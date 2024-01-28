using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Ecommerce.Order.Core.Inventorys.EventStream;
using Sample.Ecommerce.Order.Core.Orders.EventStream;
using Sample.Ecommerce.Order.Core.Orders.Structs;

namespace Sample.Ecommerce.Order.Core;

public static class Configuration
{
    public static IServiceCollection AddCore(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddScoped<IOrderStructProcessor, OrderProcessor>()            
            .Decorate<IOrderStructProcessor, StockProcessor>()
            .Decorate<IOrderStructProcessor, BankAccountProcessor>();

        services.AddScoped<IStockRepository, InventoryStreamProcessor>();
        services.AddScoped<IOrderProcessorEvents, OrderStreamProcessor>();

        services.AddBus(configuration);     

        return services;
    }    
}
