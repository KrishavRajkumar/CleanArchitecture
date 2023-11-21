using Microsoft.AspNetCore.Mvc;
using MediatR;
using InfoTrack.Refactoring.Application.Features.Positions.Commands.CreatePosition;

namespace InfoTrack.Refactoring.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PositionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "AddPosition")]
        public async Task<ActionResult<CreatePositionCommandResponse>> Create([FromBody] CreatePositionCommand createPositionCommand)
        {
            var response = await _mediator.Send(createPositionCommand);
            return Ok(response);
        }
    }
}
