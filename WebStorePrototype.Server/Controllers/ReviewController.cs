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
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Review?>> Get(Guid id)
        {
            var review = await _mediator.Send(new GetByIdQuery<Review>(id));
            return Ok(review);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAll()
        {
            var reviews = await _mediator.Send(new GetAllQuery<Review>());
            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult<Review>> Create(Review review)
        {
            await _mediator.Send(new CreateCommand<Review>(review));
            return Created();
        }

        [HttpPut]
        public async Task<ActionResult<Review>> Update(Review review)
        {
            await _mediator.Send(new UpdateCommand<Review>(review));
            return Ok(review);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCommand<Review>(id));
            return NoContent();
        }
    }
} 

