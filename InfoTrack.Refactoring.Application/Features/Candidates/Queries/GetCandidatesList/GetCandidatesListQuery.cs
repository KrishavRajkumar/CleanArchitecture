using MediatR;

namespace InfoTrack.Refactoring.Application.Features.Candidates.Queries.GetCandidatesList
{
    public class GetCandidatesListQuery : IRequest<List<CandidateListVm>>
    {
    }
}
