using EnterpriseService.Messaging.EventBus;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://example.com",
                                              "http://www.contoso.com");
                      });
});

builder.Services.AddMassTransit(x =>
{
    var configuration = builder.Configuration;

    //var rabbitHost = configuration.GetValue<string>("MessageBus:Uri");
    //var username = configuration.GetValue<string>("MessageBus:Username");
    //var password = configuration.GetValue<string>("MessageBus:Password");

    x.SetKebabCaseEndpointNameFormatter();
    //x.UsingRabbitMq((context, config) =>
    //{
    //    config.ConfigureEndpoints(context);
    //    config.Host(new Uri(rabbitHost), h =>
    //    {
    //        h.Username(username);
    //        h.Password(password);
    //    });

    //});
    x.UsingRabbitMq((context, cfg) =>
    {
        var configuration = context.GetRequiredService<IConfiguration>();
        var host = configuration.GetConnectionString("messaging");
        cfg.Host(host);
        cfg.ConfigureEndpoints(context);
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

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
