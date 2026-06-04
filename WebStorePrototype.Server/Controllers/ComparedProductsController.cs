using DAL.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStorePrototype.Server.Features.CartProduct.Commands;
using WebStorePrototype.Server.Features.ComparedProduct.Commands;
using WebStorePrototype.Server.Features.ComparedProduct.Queries;
using WebStorePrototype.Server.Models.DTO_s;

namespace WebStorePrototype.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComparedProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ComparedProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CartProductDTO>> GetComapedProductAsync([FromQuery] Guid id)
        {
            return Ok(await _mediator.Send(new GetComparedProductQuery(id)));
        }

        [HttpPost("{productId:guid}")]
        public async Task<IActionResult> AddComparedProduct([FromQuery] Guid productId, [FromQuery] String? userId)
        {
            await _mediator.Send(new AddComparedProductCommand(productId, userId));
            return Created();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveComparedProduct([FromQuery] Guid id)
        {
            await _mediator.Send(new RemoveComparedProductCommand(id));
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComparedProduct([FromQuery] ComparedProduct product)
        {
            return Ok(await _mediator.Send(new UpdateComparedProductCommand(product)));
        }
    }
}
