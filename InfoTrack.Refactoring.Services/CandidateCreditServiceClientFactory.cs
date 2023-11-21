using InfoTrack.Refactoring.Application;
using InfoTrack.Refactoring.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTrack.Refactoring.Services
{
   
    public class CandidateCreditServiceClientFactory 
    {    

        private readonly IServiceProvider _serviceProvider;

        public CandidateCreditServiceClientFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICandidateCreditService CreateCreditServiceClient(string type)
        {
            if (type.ToLower() == Constants.EntityType.Candidate)
            {
                return _serviceProvider.GetService<CandidateCreditServiceClient>();
            }
            
            throw new NotSupportedException("Client type not supported.");
           
        }
    }      
    
}