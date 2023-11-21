using AutoMapper;
using FluentValidation;
using InfoTrack.Refactoring.Application.Contracts.Persistence;
using InfoTrack.Refactoring.Application.Features.Candidates.Commands.CreateCandidate;
using InfoTrack.Refactoring.Application.Features.Positions.Commands.CreatePosition;
using InfoTrack.Refactoring.Domain.Entities;
using MediatR;

namespace InfoTrack.Refactoring.Application.Features.Positions.Commands.CreatePosition
{
    public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, CreatePositionCommandResponse>
    {
      
        private readonly IAsyncRepository<Position> _positionRepository;   
        private readonly IMapper _mapper;

        public CreatePositionCommandHandler(IMapper mapper, IAsyncRepository<Position> positionRepository)
        {
            _mapper = mapper;
            _positionRepository = positionRepository;   
           
        }        

        public async Task<CreatePositionCommandResponse> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
        {
            var CreatePositionCommandResponse = new CreatePositionCommandResponse();
            var validator = new CreatePositionCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                CreatePositionCommandResponse.Success = false;
                CreatePositionCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    CreatePositionCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (CreatePositionCommandResponse.Success)
            {
                var @position = _mapper.Map<Position>(request);
                position = await _positionRepository.AddAsync(@position);
                CreatePositionCommandResponse.position = _mapper.Map<CreatePositionDto>(position);
            }
            return CreatePositionCommandResponse;
        }

       
    }
}
