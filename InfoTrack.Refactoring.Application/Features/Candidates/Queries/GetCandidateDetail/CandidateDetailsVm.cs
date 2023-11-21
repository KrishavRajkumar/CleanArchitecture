using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTrack.Refactoring.Application.Features.Candidates.Queries.GetCandidateDetail
{
    public class CandidateDetailsVm
    {
        public int CandidateId { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public int Credit { get; set; }
        public int PositionId { get; set; }
        public bool RequireCreditCheck { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
    }
}
