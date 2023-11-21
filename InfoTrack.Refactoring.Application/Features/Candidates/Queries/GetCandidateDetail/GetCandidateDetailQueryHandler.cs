using AutoMapper;
using InfoTrack.Refactoring.Application.Contracts.Persistence;
using MediatR;

namespace InfoTrack.Refactoring.Application.Features.Candidates.Queries.GetCandidateDetail
{
    public class GetCandidateDetailQueryHandler : IRequestHandler<GetCandidateDetailQuery, CandidateDetailsVm>
    {
        private readonly IMapper _mapper;
        private readonly ICandidateRepository _candidateRepository;
        public GetCandidateDetailQueryHandler(IMapper mapper, ICandidateRepository candidateRepository)
        {
            _mapper = mapper;
            _candidateRepository = candidateRepository;
        }

        public async Task<CandidateDetailsVm> Handle(GetCandidateDetailQuery request, CancellationToken cancellationToken)
        {
            var @candidate = await _candidateRepository.GetByIdAsync(request.Id);
            var candidateDetailsVm = _mapper.Map<CandidateDetailsVm>(@candidate);
            return candidateDetailsVm;
        }
    }
}
