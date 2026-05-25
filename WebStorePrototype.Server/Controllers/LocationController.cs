using AutoMapper;
using DAL.Models;
using DAL.Repos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebStorePrototype.Server.Features.Base;


namespace WebStorePrototype.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Location?>> Get(Guid id)
        {
            var location = await _mediator.Send(new GetByIdQuery<Location>(id));
            if(location == null) return NotFound();
            return Ok(location);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetAll()
        {
            var locations = await _mediator.Send(new GetAllQuery<Location>());
            return Ok(locations);
        }

        [HttpPost]
        public async Task<ActionResult<Location>> Create(Location location)
        {
            await _mediator.Send(new CreateCommand<Location>(location));
            return Created();
        }

        [HttpPut]
        public async Task<ActionResult<Location>> Update(Location location)
        {
            var updatedLocation = await _mediator.Send(new UpdateCommand<Location>(location));
            return Ok(location);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCommand<Location>(id));
      
            return NoContent();

        }

    }
}
