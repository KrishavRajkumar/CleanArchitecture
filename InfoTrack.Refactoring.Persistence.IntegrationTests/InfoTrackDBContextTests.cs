using InfoTrack.Refactoring.Domain.Entities;
using InfoTrack.Refactoring.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace InfoTrack.Refactoring.Persistence.IntegrationTests
{
    public class InfoTrackDBContextTests
    {
        private readonly InfoTrackDbContext _infoTrackDbContext;
        private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
        private readonly string _loggedInUserId;

        public InfoTrackDBContextTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<InfoTrackDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _loggedInUserId = "Raj";
            _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            _loggedInUserServiceMock.Setup(m => m.UserId).Returns(_loggedInUserId);
            _infoTrackDbContext = new InfoTrackDbContext(dbContextOptions);
        }

        [Fact]
        public async void Save_SetCreatedByProperty()
        {
            var position = new Position() { PositionId = 1, Name = "SecuritySpecialist" };
            _infoTrackDbContext.Positions.Add(position);
            await _infoTrackDbContext.SaveChangesAsync();
            position.CreatedBy.ShouldBe(_loggedInUserId);
            position.Name.ShouldBe("SecuritySpecialist");
        }
    }
}
