using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Ecommerce.Core.API.Options;
using Sample.Ecommerce.Order.Core.Orders.Handlers;
using Sample.Ecommerce.Order.Core.Orders.Machine;
using Sample.Ecommerce.Order.Core.Orders.Machine.Activity;
using Sample.Ecommerce.Order.Core.Orders.Workers;

namespace Sample.Ecommerce.Order.Core;

public static class BusConfiguration
{    

    public static IServiceCollection AddBus(this IServiceCollection services,
        IConfiguration configuration)
    {
        BusOptions busOptions = configuration.GetSection(BusOptions.Key).Get<BusOptions>()!;
        MongoOptions mongoOptions = configuration.GetSection(MongoOptions.Key).Get<MongoOptions>()!;

        services.AddScoped<AcceptOrderMachineActivity>();

        services.AddMassTransit(e =>
        {
            e.SetKebabCaseEndpointNameFormatter();

            e.AddConsumer<FulfillOrderConsumer>(typeof(FulfillOrderConsumerDefinition));
            e.AddConsumer<SubmitOrderConsumer>(typeof(SubmitOrderConsumerDefinition));            

            e.AddSagaStateMachine<OrderStateMachine, OrderState>()
                .MongoDbRepository(r =>
                {
                    r.Connection = mongoOptions.Connection;
                    r.DatabaseName = mongoOptions.DatabaseName;
                    r.CollectionName = nameof(OrderStateMachine);
                });

            e.AddActivitiesFromNamespaceContaining<AllocateInventoryActivity>();            

            e.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(busOptions.Host, busOptions.VirtualHost, h =>
                {
                    h.Username(busOptions.UserName);
                    h.Password(busOptions.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}

