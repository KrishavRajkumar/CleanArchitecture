using InfoTrack.Refactoring.Domain.Common;

namespace InfoTrack.Refactoring.Domain.Entities
{
    public class Candidate : AuditableEntity
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
