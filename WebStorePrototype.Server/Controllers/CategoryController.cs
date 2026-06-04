using DAL.Models;
using DAL.Repos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WebStorePrototype.Server.Features.Base;
using WebStorePrototype.Server.Features.CrudHandlers;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Category?>> GetAsync(Guid id)
        {
            var category = await _mediator.Send(new GetByIdQuery<Category>(id));
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllAsync()
        {
            var categories = await _mediator.Send(new GetAllQuery<Category>());
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _mediator.Send(new CreateCommand<Category>(category));
            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Category category)
        {
            await _mediator.Send(new UpdateCommand<Category>(category));
            return Ok(category);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCommand<Category>(id));
            return NoContent();

        }

        [HttpGet("batch")]
        public async Task<ActionResult<IEnumerable<Category>>> Batch(List<Guid> ids)
        {
            var categories = await _mediator.Send(new GetBatchQuery<Category>(ids));
            return Ok(categories);
        }
    }
}
