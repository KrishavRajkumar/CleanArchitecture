namespace InfoTrack.Refactoring.Application.Contracts.Services
{
    public interface ICandidateCreditService
    {
        int GetCredit(string firstname, string surname, DateTime dateOfBirth);
    }
}