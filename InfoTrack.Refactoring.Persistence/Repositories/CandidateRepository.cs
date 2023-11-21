using InfoTrack.Refactoring.Application.Contracts.Persistence;
using InfoTrack.Refactoring.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace InfoTrack.Refactoring.Persistence.Repositories
{
    public class CandidateRepository : BaseRepository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(InfoTrackDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Candidate>> GetCandidates()
        {
            var candidatesWithPositions = await _dbContext.Candidates
             .Join(
                 _dbContext.Positions,
                 candidate => candidate.PositionId,
                 position => position.PositionId,
                 (candidate, position) => new { Candidate = candidate, Position = position }
             )
             .Select(result => result.Candidate) // Selecting only the Candidate part
             .ToListAsync();

            return candidatesWithPositions;
        }
    }
}
