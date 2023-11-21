using AutoMapper;
using InfoTrack.Refactoring.Application.Contracts.Persistence;
using InfoTrack.Refactoring.Application.Contracts.Services;
using InfoTrack.Refactoring.Application.Features.Candidates.Commands.CreateCandidate;
using InfoTrack.Refactoring.Application.Profiles;
using InfoTrack.Refactoring.Application.UnitTests.Mocks;
using InfoTrack.Refactoring.Domain.Entities;
using Moq;
using Shouldly;

namespace InfoTrack.Refactoring.Application.UnitTests.Candidates.Commands
{
    public class CreateCandidateTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Candidate>> _mockCandidateRepository;
        private readonly Mock<IAsyncRepository<Position>> _mockPositionRepository;
        private readonly Mock<ICandidateCreditService> _mockCandidateService;
        public CreateCandidateTests()
        {
            _mockCandidateRepository = RepositoryMocks.GetCandidateRepsoitory();
            _mockPositionRepository = new Mock<IAsyncRepository<Position>>();
            _mockCandidateService = new Mock<ICandidateCreditService>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mockCandidateService.Setup(service => service.GetCredit("TestSecuritySpecialist", "CreditLessThan500", new DateTime(2001, 11, 5))).Returns(700);
            _mockCandidateService.Setup(service => service.GetCredit("TestSecuritySpecialist", "CreditGreaterThan500", new DateTime(2001, 11, 5))).Returns(1200);
            _mockCandidateService.Setup(service => service.GetCredit("TestFeatureDeveloper", "CreditLessThan500", new DateTime(2001, 11, 5))).Returns(300);
            _mockCandidateService.Setup(service => service.GetCredit("TestFeatureDeveloper", "CreditGreaterThan500", new DateTime(2001, 11, 5))).Returns(750);
            //_mockCandidateService.Setup(service => service.GetCredit("TestNotInPosition", "CreditDoesNotMatter", new DateTime(2001, 11, 5))).Returns(750);


            _mockPositionRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                if (id == 1)
                {
                    return new Position { PositionId = id, Name = "SecuritySpecialist" };
                }
                if (id == 2)
                {
                    return new Position { PositionId = id, Name = "FeatureDeveloper" };
                }
                return null;
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task When_Candidate_Is_SecuritySpecialist_and_Credit_Score_Is_Lessthan_500_then_call_should_be_failed()
        {
            var handler = new CreateCandidateCommandHandler(_mapper, _mockCandidateRepository.Object, _mockPositionRepository.Object, _mockCandidateService.Object);
            var response = await handler.Handle(new CreateCandidateCommand() { Firstname = "TestSecuritySpecialist", Surname = "CreditLessThan500", DateOfBirth = new DateTime(2001, 11, 5), Email = "test5@gmail.com", PositionId = 1 }, CancellationToken.None);           
            response.Message.ShouldBe("Credit check failed for the candidate");           
        }
        [Fact]
        public async Task When_Candidate_Is_SecuritySpecialist_and_Credit_Score_Is_GreaterThan_500_then_call_should_be_success()
        {
            var handler = new CreateCandidateCommandHandler(_mapper, _mockCandidateRepository.Object, _mockPositionRepository.Object, _mockCandidateService.Object);
            var response = await handler.Handle(new CreateCandidateCommand() { Firstname = "TestSecuritySpecialist", Surname = "CreditGreaterThan500", DateOfBirth = new DateTime(2001, 11, 5), Email = "test5@gmail.com", PositionId = 1 }, CancellationToken.None);
            var allCandidates = await _mockCandidateRepository.Object.ListAllAsync();
            allCandidates.Count.ShouldBe(5);
        }
        [Fact]
        public async Task When_Candidate_Is_FeautureDeveloper_and_Credit_Score_Is_Lessthan_500_then_call_should_be_failed()
        {
            var handler = new CreateCandidateCommandHandler(_mapper, _mockCandidateRepository.Object, _mockPositionRepository.Object, _mockCandidateService.Object);
            var response = await handler.Handle(new CreateCandidateCommand() { Firstname = "TestFeatureDeveloper", Surname = "CreditLessThan500", DateOfBirth = new DateTime(2001, 11, 5), Email = "test5@gmail.com", PositionId = 2 }, CancellationToken.None);
            response.Message.ShouldBe("Credit check failed for the candidate");
        }

        [Fact]
        public async Task When_Candidate_Is_FeatureDeveloper_and_Credit_Score_Is_Greaterthan_500_then_call_should_be_success()
        {
            var handler = new CreateCandidateCommandHandler(_mapper, _mockCandidateRepository.Object, _mockPositionRepository.Object, _mockCandidateService.Object);
            var response = await handler.Handle(new CreateCandidateCommand() { Firstname = "TestFeatureDeveloper", Surname = "CreditGreaterThan500", DateOfBirth = new DateTime(2001, 11, 5), Email = "test5@gmail.com", PositionId = 2 }, CancellationToken.None);
            var allCandidates = await _mockCandidateRepository.Object.ListAllAsync();
            allCandidates.Count.ShouldBe(5);
        }

        [Fact]
        public async Task When_Candidate_Is_Not_In_The_Specified_Position_and_Credit_Score_DoesNot_Needed_then_call_should_be_success()
        {
            var handler = new CreateCandidateCommandHandler(_mapper, _mockCandidateRepository.Object, _mockPositionRepository.Object, _mockCandidateService.Object);
            var response = await handler.Handle(new CreateCandidateCommand() { Firstname = "TestNotInPosition", Surname = "CreditDoesNotMatter", DateOfBirth = new DateTime(2001, 11, 5), Email = "test5@gmail.com", PositionId = 3 }, CancellationToken.None);
            var allCandidates = await _mockCandidateRepository.Object.ListAllAsync();
            allCandidates.Count.ShouldBe(5);
        }


        [Fact]
        public async Task When_Candidate_Entered_incorrect_email_format_validation_error_should_be_thrown()
        {
            var handler = new CreateCandidateCommandHandler(_mapper, _mockCandidateRepository.Object, _mockPositionRepository.Object, _mockCandidateService.Object);
            var response =await handler.Handle(new CreateCandidateCommand() { Firstname = "Test5",Surname = "Test5", DateOfBirth = DateTime.Now.AddYears(-20), Email = "test5@@@@", PositionId = 1 }, CancellationToken.None);
            response.ValidationErrors?.Count.ShouldBe(1);
            response?.ValidationErrors?[0].ShouldBe("Email is required or incorrect Email");
        }

        [Fact]
        public async Task When_Candidate_Is_Less_Than_18Years_Old_Validation_error_should_be_thrown()
        {
            var handler = new CreateCandidateCommandHandler(_mapper, _mockCandidateRepository.Object, _mockPositionRepository.Object, _mockCandidateService.Object);
            var response = await handler.Handle(new CreateCandidateCommand() { Firstname = "Test5",Surname = "Test5", DateOfBirth = DateTime.Now.AddYears(-10), Email = "test5@gmail.com", PositionId = 1 }, CancellationToken.None);
            response.ValidationErrors?.Count.ShouldBe(1);
            response?.ValidationErrors?[0].ShouldBe("Candidate should be at least 18 years old");
        }
        
    }
}
