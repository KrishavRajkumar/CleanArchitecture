using InfoTrack.Refactoring.Domain.Entities;
using InfoTrack.Refactoring.Persistence;

namespace InfoTrack.Refactoring.API.IntegrationTests.Base
{
    public class Utilities
    {
        public static void InitializeDbForTests(InfoTrackDbContext context)
        {          

            context.Candidates.Add(new Candidate
            {
                CandidateId = 1,
                Firstname = "Test1",
                Surname = "Test1",
                Email = "Test1@gmail.com",
                PositionId = 1,
            });
            context.Candidates.Add(new Candidate
            {
                CandidateId = 2,
                Firstname = "Test2",
                Surname = "Test2",
                Email = "Test2@gmail.com",
                PositionId = 1,
            });
            context.Candidates.Add(new Candidate
            {
                CandidateId = 3,
                Firstname = "Test3",
                Surname = "Test3",
                Email = "Test3@gmail.com",
                PositionId = 2,
            });
            context.Candidates.Add(new Candidate
            {
                CandidateId = 4,
                Firstname = "Test4",
                Surname = "Test4",
                Email = "Test4@gmail.com",
                PositionId = 3,
            });

            context.SaveChanges();
        }
    }
}
