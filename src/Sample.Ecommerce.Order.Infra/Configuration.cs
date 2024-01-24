using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Ecommerce.Order.Infra;

public static class Configuration
{
    public static IServiceCollection AddInfrastruct(this ServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration["connectionString"] 
            ?? throw new ArgumentNullException("connection string argument does not informed.");


        services.AddMarten(options =>
        {
            options.Connection(connectionString);
            options.Events.StreamIdentity = Marten.Events.StreamIdentity.AsGuid;
        });

        services.AddDbContext<ContextEcommerce>((sp, options) =>
        {                        
        }, ServiceLifetime.Transient);

        return services;
    }
}
