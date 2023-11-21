using InfoTrack.Refactoring.Domain.Common;

namespace InfoTrack.Refactoring.Domain.Entities
{
    public class Position : AuditableEntity
    {
        public int PositionId { get; set; }
        public string? Name { get; set; }
        public int Status { get; set; }

    }
}
