using InfoTrack.Refactoring.Application.Contracts.Persistence;
using InfoTrack.Refactoring.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoTrack.Refactoring.Persistence.Repositories
{
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(InfoTrackDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Candidate>> GetCandidates()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Position>> GetPositions()
        {
            return await _dbContext.Positions.ToListAsync();                 
        }
    }
}
