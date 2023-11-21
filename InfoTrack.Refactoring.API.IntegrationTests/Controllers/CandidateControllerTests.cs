using InfoTrack.Refactoring.API.IntegrationTests.Base;
using InfoTrack.Refactoring.Application.Features.Candidates.Queries.GetCandidatesList;
using System.Text.Json;


namespace InfoTrack.Refactoring.API.IntegrationTests.Controllers
{
    public class CandidateControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public CandidateControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessResult()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/candidate/all");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<CandidateListVm>>(responseString);

            Assert.IsType<List<CandidateListVm>>(result);            
        }
    }
}
