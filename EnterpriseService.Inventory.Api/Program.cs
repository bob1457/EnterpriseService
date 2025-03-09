using EnterpriseService.Inventory.Api.EventHandlers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddLogging();
//builder.Services.AddScoped<InventoryServiceConsumer>();

builder.Services.AddMassTransit(x =>
{
    var configuration = builder.Configuration;

    var rabbitHost = configuration.GetValue<string>("MessageBus:Uri");
    var username = configuration.GetValue<string>("MessageBus:Username");
    var password = configuration.GetValue<string>("MessageBus:Password");

    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumer<InventoryServiceConsumer>(); // thi is only required for consumers
    x.UsingRabbitMq((context, config) =>
    {
        config.ConfigureEndpoints(context);
        config.Host(new Uri(rabbitHost), h =>
        {
            h.Username(username);
            h.Password(password);
        });
    });
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
