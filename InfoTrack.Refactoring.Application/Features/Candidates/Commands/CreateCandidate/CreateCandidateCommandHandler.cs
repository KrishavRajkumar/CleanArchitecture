using AutoMapper;
using InfoTrack.Refactoring.Application.Contracts.Persistence;
using InfoTrack.Refactoring.Domain.Entities;
using MediatR;
using InfoTrack.Refactoring.Application.Contracts.Services;
using System.ServiceModel.Channels;

namespace InfoTrack.Refactoring.Application.Features.Candidates.Commands.CreateCandidate
{
    public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, CreateCandidateCommandResponse>
    {
        private readonly IAsyncRepository<Candidate> _candidateRepository;
        private readonly IAsyncRepository<Position> _positionRepository;
        private readonly ICandidateCreditService _candidateCreditService;

        private readonly IMapper _mapper;

        public CreateCandidateCommandHandler(IMapper mapper, IAsyncRepository<Candidate> candidateRepository, IAsyncRepository<Position> positionRepository, ICandidateCreditService candidateCreditService)
        {
            _mapper = mapper;
            _candidateRepository = candidateRepository;
            _positionRepository = positionRepository;   
            _candidateCreditService= candidateCreditService;
        }
        public async Task<CreateCandidateCommandResponse> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
           
            var createCandidateCommandResponse = new CreateCandidateCommandResponse();
            var validator = new CreateCandidateCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createCandidateCommandResponse.Success = false;
                createCandidateCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createCandidateCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (createCandidateCommandResponse.Success)
            {
                var position = await _positionRepository.GetByIdAsync(request.PositionId);  
                var @candidate = _mapper.Map<Candidate>(request);
                @candidate = IsCreditCheckRequiredForCandidate(@candidate, position?.Name);                

                if(@candidate.RequireCreditCheck && @candidate.Credit < 500)
                {
                    return new CreateCandidateCommandResponse { Success = false, Message = "Credit check failed for the candidate" };
                }                                                  

                candidate = await _candidateRepository.AddAsync(@candidate);                
                if (candidate == null)
                {
                    createCandidateCommandResponse.Success = false;
                    createCandidateCommandResponse.Message = "Error creating Candidate. Pleae check logs";
                    return createCandidateCommandResponse;
                }               
                createCandidateCommandResponse.Candidate = _mapper.Map<CreateCandidateDto>(candidate);                 
            }

            return createCandidateCommandResponse;
        }       

        private Candidate IsCreditCheckRequiredForCandidate(Candidate candidate, string? positionName)
        {
            switch (positionName)
            {
                case Constants.Positions.SecuritySpecialist:
                    candidate.RequireCreditCheck = true;
                    candidate.Credit = _candidateCreditService.GetCredit(candidate.Firstname, candidate.Surname, candidate.DateOfBirth) / 2;
                    break;

                case Constants.Positions.FeatureDeveloper:
                    candidate.RequireCreditCheck = true;
                    candidate.Credit = _candidateCreditService.GetCredit(candidate.Firstname, candidate.Surname, candidate.DateOfBirth);
                    break;

                default:
                    candidate.RequireCreditCheck = false;
                    break;
            }
            return candidate;
        }
    }
}
