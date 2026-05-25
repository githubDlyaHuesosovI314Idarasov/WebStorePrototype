using DAL.Models;
using DAL.Repos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using WebStorePrototype.Server.Features.Base;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator; 

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Stock?>> Get(Guid id)
        {
            var stock = await _mediator.Send(new GetByIdQuery<Stock>(id));
            return Ok(stock);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetAll()
        {
            var stocks = await _mediator.Send(new GetAllQuery<Stock>());
            return Ok(stocks);

        }

        [HttpPost]
        public async Task<ActionResult<Stock>> Create(Stock stock)
        {
            await _mediator.Send(new CreateCommand<Stock>(stock));
            return Created();
        }

        [HttpPut]
        public async Task<ActionResult<Stock>> Update(Stock stock)
        {
            await _mediator.Send(new UpdateCommand<Stock>(stock));
            return Ok(stock);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCommand<Stock>(id));
            return NoContent();

        }
    }
}
