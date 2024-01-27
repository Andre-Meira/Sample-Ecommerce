using Sample.Ecommerce.Core.API.Observability;
using Sample.Ecommerce.Core.API.Middleware;
using Sample.Ecommerce.Order.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureLogging();
builder.Services.AddTracing("Ecommerce", builder.Configuration);

builder.Services.AddInfrastruct(builder.Configuration);

builder.Services.AddTransient<ErrorHandlerMiddleware>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
