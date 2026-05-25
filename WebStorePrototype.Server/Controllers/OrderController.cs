using Microsoft.AspNetCore.Mvc;
using WebStorePrototype.Server.Services;
using Order = DAL.Models.Order;
using StackExchange.Redis;
using DAL.Repos;
using Microsoft.EntityFrameworkCore;
using MediatR;
using WebStorePrototype.Server.Features.Base;
using WebStorePrototype.Server.Features.CrudHandlers;

namespace WebStorePrototype.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Order?>> Get(Guid id)
        {
            var order = await _mediator.Send(new GetByIdQuery<Order>(id));
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            var orders = await _mediator.Send(new GetAllQuery<Order>());
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            await _mediator.Send(new CreateCommand<Order>(order));
            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Order order)
        {
            await _mediator.Send(new UpdateCommand<Order>(order));
            return Ok(order);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCommand<Order>(id));
            return NoContent();
        }
    }
}
