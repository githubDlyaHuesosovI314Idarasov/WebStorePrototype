using AutoMapper;
using Contracts;
using DAL.Models;
using DAL.Repos;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WebStorePrototype.Server.Features.Base;
using WebStorePrototype.Server.Features.CrudHandlers;
using WebStorePrototype.Server.Services;
using Order = DAL.Models.Order;

namespace WebStorePrototype.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public OrderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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

        [HttpGet("batch")]
        public async Task<ActionResult<IEnumerable<Order>>> Batch(List<Guid> ids)
        {
            var orders = await _mediator.Send(new GetBatchQuery<Order>(ids));
            return Ok(orders);
        }

        [HttpPost("publish")]
        public async Task Publish(Order order)
        {
            var createdOrder = _mapper.Map<OrderCreated>(order);
            await _mediator.Send(new PublishCommand<OrderCreated>(createdOrder));
        }
    }
}
