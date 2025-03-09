using EnterpriseService.Messaging.Events;
using MassTransit;

namespace EnterpriseService.Inventory.Api.EventHandlers
{
    public sealed class InventoryServiceConsumer : IConsumer<ProductCreatedEvent>
    {
        private readonly ILogger<InventoryServiceConsumer> _logger;

        public InventoryServiceConsumer(ILogger<InventoryServiceConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            _logger.LogInformation($"Product created: {context.Message.Id}");

            return Task.CompletedTask;
        }
    }
}
