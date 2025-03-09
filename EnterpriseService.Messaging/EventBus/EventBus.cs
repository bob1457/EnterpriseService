using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseService.Messaging.EventBus
{
    public sealed class EventBus : IEventBus//, IDisposable
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        public Task PublishAsync<T>(T message, CancellationToken cancellation = default) 
            where T : class =>        
            _publishEndpoint.Publish<T>(message, cancellation);
        
    }
}
