using InfoTrack.Refactoring.Application.Contracts.Services;
using InfoTrack.Refactoring.Services;

public class CandidateCreditService : ICandidateCreditService
{
    private readonly CandidateCreditServiceClientFactory _clientFactory;

    public CandidateCreditService(CandidateCreditServiceClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public int GetCredit(string firstname, string surname, DateTime dateOfBirth)
    {
        var client = _clientFactory.CreateCreditServiceClient("candidate");//give the type
        return 1000;
        //return client.GetCredit("","", DateTime.Now);      
    }

   
}
