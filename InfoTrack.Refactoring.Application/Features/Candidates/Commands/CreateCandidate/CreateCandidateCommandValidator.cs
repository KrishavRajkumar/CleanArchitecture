using FluentValidation;

namespace InfoTrack.Refactoring.Application.Features.Candidates.Commands.CreateCandidate
{
    public class CreateCandidateCommandValidator : AbstractValidator<CreateCandidateCommand>
    {
        public CreateCandidateCommandValidator()
        {
            RuleFor(candidate => candidate.Firstname).NotEmpty().WithMessage("{PropertyName} is required.");            
            RuleFor(candidate => candidate.Surname).NotEmpty().WithMessage("{PropertyName} is required.");            
            RuleFor(candidate => candidate.DateOfBirth).Must(BeAtLeast18YearsOld).WithMessage("Candidate should be at least 18 years old");
            RuleFor(candidate => candidate.Email).NotEmpty().EmailAddress().WithMessage("{PropertyName} is required or incorrect {PropertyName}"); ;
        }

        private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;

            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            {
                age--;
            }

            return age >= 18;
        }
    }
}
