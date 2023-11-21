using FluentValidation;

namespace InfoTrack.Refactoring.Application.Features.Positions.Commands.CreatePosition
{
    public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
    {
        public CreatePositionCommandValidator()
        {
            RuleFor(candidate => candidate.Name).NotEmpty().WithMessage("{PropertyName} is required.");           
        }
        
    }
}
