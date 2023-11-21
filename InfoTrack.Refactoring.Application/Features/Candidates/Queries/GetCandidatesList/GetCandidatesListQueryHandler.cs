using AutoMapper;
using InfoTrack.Refactoring.Application.Contracts.Persistence;
using InfoTrack.Refactoring.Domain.Entities;
using MediatR;

namespace InfoTrack.Refactoring.Application.Features.Candidates.Queries.GetCandidatesList
{
    public class GetCandidatesListQueryHandler : IRequestHandler<GetCandidatesListQuery, List<CandidateListVm>>
    {
        private readonly IAsyncRepository<Candidate> _candidateRepository;
        private readonly IMapper _mapper;

        public GetCandidatesListQueryHandler(IMapper mapper, IAsyncRepository<Candidate> candidateRepository)
        {
            _mapper = mapper;
            _candidateRepository = candidateRepository;
        }

        public async Task<List<CandidateListVm>> Handle(GetCandidatesListQuery request, CancellationToken cancellationToken)
        {
            var allCandidates = (await _candidateRepository.ListAllAsync()).OrderBy(x => x.Firstname);
            return _mapper.Map<List<CandidateListVm>>(allCandidates);
        }

    }
}
