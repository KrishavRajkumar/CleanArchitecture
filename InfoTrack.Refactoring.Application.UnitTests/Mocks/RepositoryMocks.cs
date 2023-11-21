using InfoTrack.Refactoring.Application.Contracts.Persistence;
using InfoTrack.Refactoring.Domain.Entities;
using Moq;

namespace InfoTrack.Refactoring.Application.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IAsyncRepository<Candidate>> GetCandidateRepsoitory()
        {
            
            var Candidates = new List<Candidate>
            {
                new Candidate
                {
                    CandidateId = 1,
                    Firstname = "Test1",
                    Surname = "Test1",
                    Email = "Test1@gmail.com",
                    PositionId = 1,
                },
                 new Candidate
                {
                    CandidateId = 2,
                    Firstname = "Test2",
                    Surname = "Test2",
                    Email = "Test2@gmail.com",
                    PositionId = 1,
                },
                new Candidate
                {
                    CandidateId = 3,
                    Firstname = "Test3",
                    Surname = "Test3",
                    Email = "Test3@gmail.com",
                    PositionId = 2,
                },
                 new Candidate
                {
                    CandidateId = 4,
                    Firstname = "Test4",
                    Surname = "Test4",
                    Email = "Test4@gmail.com",
                    PositionId = 3,
                },
            };

            var mockCandidateReposiroty = new Mock<IAsyncRepository<Candidate>>();
            mockCandidateReposiroty.Setup(repo => repo.ListAllAsync()).ReturnsAsync(Candidates);

            mockCandidateReposiroty.Setup(repo => repo.AddAsync(It.IsAny<Candidate>())).ReturnsAsync(
                (Candidate candidate) =>
                {
                    Candidates.Add(candidate);
                    return candidate;
                });

            return mockCandidateReposiroty;
        }

        public static Mock<IAsyncRepository<Position>> GetPositionRepository()
        {

            var Positions = new List<Position>
            {
                new Position
                {
                   PositionId=1,
                   Name ="SecuritySepcialist"
                },
                 new Position
                {
                   PositionId=2,
                   Name ="FeatureDeveloper"
                },

            };

            var mockPositionRepository = new Mock<IAsyncRepository<Position>>();
            mockPositionRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(Positions);
            mockPositionRepository.Setup(repo => repo.AddAsync(It.IsAny<Position>())).ReturnsAsync(
                (Position position) =>
                {
                    Positions.Add(position);
                    return position;
                });

            return mockPositionRepository;
        }
    }
}
