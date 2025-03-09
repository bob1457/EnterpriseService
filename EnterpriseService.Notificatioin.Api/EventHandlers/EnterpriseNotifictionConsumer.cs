using EnterpriseService.Messaging.Events;
using MassTransit;

namespace EnterpriseService.Notificatioin.Api.EventHandlers
{
    public class EnterpriseNotifictionConsumer : IConsumer<ProductCreatedEvent>
    {
        private readonly ILogger<EnterpriseNotifictionConsumer> _logger;

        public EnterpriseNotifictionConsumer(ILogger<EnterpriseNotifictionConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            _logger.LogInformation($"Product created notificaiton: {context.Message.Id}");

            return Task.CompletedTask;
        }
    }
}
