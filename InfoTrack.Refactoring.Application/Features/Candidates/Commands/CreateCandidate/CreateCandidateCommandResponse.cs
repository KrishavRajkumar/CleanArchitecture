using InfoTrack.Refactoring.Application.Responses;

namespace InfoTrack.Refactoring.Application.Features.Candidates.Commands.CreateCandidate
{
    public class CreateCandidateCommandResponse : BaseResponse
    {
        public CreateCandidateCommandResponse() : base()
        {

        }

        public CreateCandidateDto Candidate { get; set; } = default!;
    }
}
