using EnterpriseService.Messaging.EventBus;
using EnterpriseService.Messaging.Events;
using EnterpriseService.OrderService.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseService.OrderService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IEventBus _eventBus; // this message implementation will be refactored in the applicaiton project later

        public OrderController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetProductList()
        {
            return Ok("Order Service");
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateOrder(Product product, CancellationToken cancellation)
        {
            if (product == null) {
                return BadRequest("Product is null");
            }

            product.Id = Guid.NewGuid();
            product.Name = product.Name;
            product.Price = product.Price;

            // Save product to database


            // Publish event to the message broker

            await _eventBus.PublishAsync(new ProductCreatedEvent
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            }, cancellation);



            return Ok("Order Created");
        }
    }
}
