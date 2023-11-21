using MediatR;


namespace InfoTrack.Refactoring.Application.Features.Candidates.Commands.CreateCandidate
{
    public class CreateCandidateCommand : IRequest<CreateCandidateCommandResponse>
    {
        public string Firstname { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;

        public int PositionId { get; set; }
    }
}
