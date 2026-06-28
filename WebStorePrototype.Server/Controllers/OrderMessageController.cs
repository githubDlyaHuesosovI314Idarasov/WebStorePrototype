using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebStorePrototype.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderMessageController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public OrderMessageController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
           // await _publishEndpoint.Publish();

            return Created();
        }
    }
}
