using DAL.Models;
using DAL.Repos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WebStorePrototype.Server.Features.Base;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductImage?>> Get(Guid id)
        {
            var productImage = await _mediator.Send(new GetByIdQuery<ProductImage>(id));
            return Ok(productImage);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductImage>>> GetAll()
        {
            var productImages = await _mediator.Send(new GetAllQuery<ProductImage>());
            return Ok(productImages);
        }

        [HttpPost]
        public async Task<ActionResult<ProductImage>> Create(ProductImage productImage)
        {
            await _mediator.Send(new CreateCommand<ProductImage>(productImage));
            return Created();
        }

        [HttpPut]
        public async Task<ActionResult<ProductImage>> Update(ProductImage productImage)
        {
            await _mediator.Send(new UpdateCommand<ProductImage>(productImage));
            return Ok(productImage);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCommand<ProductImage>(id));
            return NoContent();
        }

    }
}
