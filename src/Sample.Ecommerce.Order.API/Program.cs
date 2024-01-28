using Sample.Ecommerce.Core.API.Observability;
using Sample.Ecommerce.Core.API.Middleware;
using Sample.Ecommerce.Order.Infra;
using Sample.Ecommerce.Order.Core;
using Sample.Ecommerce.Core.API.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLogginSerilog("Ecommerce", builder.Configuration);

builder.Services.AddOptions();
builder.Services.Configure<BusOptions>(builder.Configuration.GetSection(BusOptions.Key));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureLogging();
builder.Services.AddTracing("Ecommerce", builder.Configuration);

builder.Services.AddInfrastruct(builder.Configuration);
builder.Services.AddCore(builder.Configuration);

builder.Services.AddTransient<ErrorHandlerMiddleware>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseReDoc(c =>
{
    c.DocumentTitle = "Ecommerce Api Documentation";
    c.SpecUrl = "/swagger/v1/swagger.json";    
});

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
