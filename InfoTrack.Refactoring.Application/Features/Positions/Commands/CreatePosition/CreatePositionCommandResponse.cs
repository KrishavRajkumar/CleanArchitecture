using InfoTrack.Refactoring.Application.Responses;

namespace InfoTrack.Refactoring.Application.Features.Positions.Commands.CreatePosition;
public class CreatePositionCommandResponse : BaseResponse
{
    public CreatePositionCommandResponse() : base()
    {

    }
    public CreatePositionDto position { get; set; } = default!;
}
