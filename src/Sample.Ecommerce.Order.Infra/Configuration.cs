using Marten;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Ecommerce.Order.Core.Inventorys.EventStream;
using Sample.Ecommerce.Order.Core.Orders.EventStream;
using Sample.Ecommerce.Order.Core.Products;
using Sample.Ecommerce.Order.Infra.Inventory;
using Sample.Ecommerce.Order.Infra.Orders;
using Sample.Ecommerce.Order.Infra.Products;

namespace Sample.Ecommerce.Order.Infra;

public static class Configuration
{
    public static IServiceCollection AddInfrastruct(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("value")
            ?? throw new ArgumentNullException("connection string argument does not informed.");

        services.AddMarten(options =>
        {
            options.Connection(connectionString);
            options.Events.StreamIdentity = Marten.Events.StreamIdentity.AsGuid;           

        }).UseLightweightSessions();

        services.AddDbContext<ContextEcommerce>((sp, options) =>
        {                        
            options.UseNpgsql(connectionString);

        }, ServiceLifetime.Transient);

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderStreamRepository, OrderStreamRepository>();
        services.AddScoped<IInventoryStreamRepository, InventoryStreamRepository>();

        return services;
    }
}
