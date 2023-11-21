using InfoTrack.Refactoring.Domain.Entities;

namespace InfoTrack.Refactoring.Application.Contracts.Persistence
{
    public interface IPositionRepository : IAsyncRepository<Position>
    {
        Task<List<Position>> GetPositions();
    }
}
