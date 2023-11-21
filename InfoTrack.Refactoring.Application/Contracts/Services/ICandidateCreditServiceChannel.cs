using System.ServiceModel;

namespace InfoTrack.Refactoring.Application.Contracts.Services
{ 
    public interface ICandidateCreditServiceChannel : ICandidateCreditService, IClientChannel
    {
    }
}