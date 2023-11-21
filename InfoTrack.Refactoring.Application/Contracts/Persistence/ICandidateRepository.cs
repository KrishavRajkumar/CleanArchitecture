using InfoTrack.Refactoring.Application.Contracts.Persistence;
using InfoTrack.Refactoring.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoTrack.Refactoring.Application.Contracts.Persistence
{
    public interface ICandidateRepository : IAsyncRepository<Candidate>
    {
        Task<List<Candidate>> GetCandidates();
    }
}
