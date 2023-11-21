using InfoTrack.Refactoring.Application.Features.Candidates.Queries.GetCandidateDetail;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTrack.Refactoring.Application.Features.Candidates.Queries.GetCandidateDetail
{
    public class GetCandidateDetailQuery : IRequest<CandidateDetailsVm>
    {
        public int Id { get; set; }
    }
}
