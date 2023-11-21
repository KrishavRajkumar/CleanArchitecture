using Asp.Versioning;
using InfoTrack.Refactoring.Application.Features.Candidates.Commands.CreateCandidate;
using InfoTrack.Refactoring.Application.Features.Candidates.Queries.GetCandidateDetail;
using InfoTrack.Refactoring.Application.Features.Candidates.Queries.GetCandidatesList;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace InfoTrack.Refactoring.Api.Controllers
{
    [ApiController]
    [ApiVersion(2.0)]
    [Route("api/v{version:apiVersion}/[controller]")]    
    public class CandidateController : ControllerBase
    {
        private readonly IMediator _mediator;
        ILogger<CandidateController> _logger;
        public CandidateController(IMediator mediator, ILogger<CandidateController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        
        [HttpPost(Name = "AddCandidate")]
        public async Task<ActionResult<CreateCandidateCommandResponse>> Create([FromBody] CreateCandidateCommand createCandidateCommand)
        {
            var response = await _mediator.Send(createCandidateCommand);
            _logger.LogInformation("test");
            return Ok(response);
        }

        [HttpGet("all", Name = "GetAllCandidates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CandidateListVm>>> GetAllCandidates()
        {
            var dtos = await _mediator.Send(new GetCandidatesListQuery());
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetCandidatesById")]
        public async Task<ActionResult<CandidateDetailsVm>> GetCandidatesById(int id)
        {
            var getCandidateDetailQuery = new GetCandidateDetailQuery() { Id = id };
            return Ok(await _mediator.Send(getCandidateDetailQuery));
        }
    }
}
