using AutoMapper;
using InfoTrack.Refactoring.Application.Contracts.Persistence;
using InfoTrack.Refactoring.Application.Contracts.Services;
using InfoTrack.Refactoring.Application.Features.Candidates.Queries.GetCandidatesList;
using InfoTrack.Refactoring.Application.Profiles;
using InfoTrack.Refactoring.Application.UnitTests.Mocks;
using InfoTrack.Refactoring.Domain.Entities;
using Moq;
using Shouldly;

namespace InfoTrack.Refactoring.Application.UnitTests.Categories.Queries
{
    public class GetCandidatesListQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Candidate>> _mockCandidateRepository;
        private readonly Mock<IAsyncRepository<Position>> _mockPositionRepository;
        private readonly Mock<ICandidateCreditService> _mockCandidateService;

        public GetCandidatesListQueryHandlerTest()
        {
            _mockCandidateRepository = RepositoryMocks.GetCandidateRepsoitory();
            _mockPositionRepository = new Mock<IAsyncRepository<Position>>();
            _mockCandidateService = new Mock<ICandidateCreditService>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetCandidatesListTest()
        {
            var handler = new GetCandidatesListQueryHandler(_mapper, _mockCandidateRepository.Object);
            var result = await handler.Handle(new GetCandidatesListQuery(), CancellationToken.None);
            result.ShouldBeOfType<List<CandidateListVm>>();
            result.Count.ShouldBe(4);
        }
    }
}