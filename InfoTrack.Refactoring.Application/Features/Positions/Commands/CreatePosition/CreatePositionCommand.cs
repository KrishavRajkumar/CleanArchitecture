using MediatR;


namespace InfoTrack.Refactoring.Application.Features.Positions.Commands.CreatePosition
{
    public class CreatePositionCommand : IRequest<CreatePositionCommandResponse>
    {
        public string Name { get; set; } = string.Empty;
    }
}
