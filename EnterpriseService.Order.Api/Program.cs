using EnterpriseService.Messaging.EventBus;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMassTransit(x =>
{
    var configuration = builder.Configuration;

    var rabbitHost = configuration.GetValue<string>("MessageBus:Uri");
    var username = configuration.GetValue<string>("MessageBus:Username");
    var password = configuration.GetValue<string>("MessageBus:Password");

    //x.AddConsumers(typeof(InventoryAdjusmentService).Assembly);
    //x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    //{
    //    config.Host(new Uri(rabbitHost), h =>
    //    {
    //        h.Username(username);
    //        h.Password(password);
    //    });
    //}));

    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((context, config) =>
    {
        config.ConfigureEndpoints(context);
        config.Host(new Uri(rabbitHost), h =>
        {
            h.Username(username);
            h.Password(password);
        });
        //config.ReceiveEndpoint("cart", ep =>
        //{
        //    ep.PrefetchCount = 16;
        //    ep.UseMessageRetry(r => r.Interval(2, new TimeSpan(0, 0, 10)));
        //    ep.ConfigureConsumer<ShoppingCartConsumer>(context);
        //});
    });
});

builder.Services.AddTransient<IEventBus, EventBus>();


var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "openapi/v1.json";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
