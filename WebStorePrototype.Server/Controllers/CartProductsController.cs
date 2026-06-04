using DAL.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStorePrototype.Server.Features.CartProduct.Commands;
using WebStorePrototype.Server.Features.CartProduct.Queries;
using WebStorePrototype.Server.Models.DTO_s;

namespace WebStorePrototype.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class CartProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CartProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CartProductDTO>> GetCartProducs([FromQuery] Guid id)
        {
            return Ok(await _mediator.Send(new GetCartProductQuery(id)));
        }

        [HttpPost("{productId:guid}")]
        public async Task<IActionResult> AddCartProduct([FromQuery] Guid productId, [FromQuery] String? userId)
        {
            await _mediator.Send(new AddCartProductCommand(productId, userId));
            return Created();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveCartProduct([FromQuery] Guid id)
        {
            await _mediator.Send(new RemoveCartProductCommand(id));
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartProduct([FromQuery] CartProduct cartProduct)
        {
            await _mediator.Send(new UpdateCartProductCommand(cartProduct));
            return Ok(cartProduct);
        }

    }
}
