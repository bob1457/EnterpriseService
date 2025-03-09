using EnterpriseService.Messaging.Events;
using MassTransit;

namespace EnterpriseService.ShippingService.Api.EventHandlers
{
    public class ShippingServiceConsumer : IConsumer<ProductCreatedEvent>
    {
        private readonly ILogger<ShippingServiceConsumer> _logger;

        public ShippingServiceConsumer(ILogger<ShippingServiceConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            _logger.LogInformation($"Product shipped: {context.Message.Id}");

            return Task.CompletedTask;
        }
    }
}
