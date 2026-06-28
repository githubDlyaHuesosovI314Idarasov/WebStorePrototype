using AutoMapper;
using Contracts;
using DAL.Models;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStorePrototype.Server.Features.Base;

namespace WebStorePrototype.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserNotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UserNotificationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost("review-publish")]
        public async Task ReviewPublish(Review review)
        {
            var reviewNotify = _mapper.Map<ReviewNotify>(review);
            await _mediator.Send(new PublishCommand<ReviewNotify>(reviewNotify));
        }

        [HttpPost("delivery-publish")]
        public async Task DeliveryPublish(DeliveryNotify deliveryNotify)
        {
            await _mediator.Send(new PublishCommand<DeliveryNotify>(deliveryNotify));
        }
        
    }
}
