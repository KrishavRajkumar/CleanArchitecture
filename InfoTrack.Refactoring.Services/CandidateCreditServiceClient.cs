using InfoTrack.Refactoring.Application.Contracts.Services;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;


[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public partial class CandidateCreditServiceClient : ClientBase<ICandidateCreditService>, ICandidateCreditService
{
    private ICandidateCreditServiceChannel _candidateTestServiceChannelImplementation;
    public CandidateCreditServiceClient() { }

    public CandidateCreditServiceClient(string endpointConfigurationName) :
        base(endpointConfigurationName)
    { }

    public CandidateCreditServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) :
        base(endpointConfigurationName, remoteAddress)
    { }

    public CandidateCreditServiceClient(Binding binding, EndpointAddress remoteAddress) :
        base(binding, remoteAddress)
    { }

    public int GetCredit(string firstname, string surname, DateTime dateOfBirth)
    {
        return base.Channel.GetCredit(firstname, surname, dateOfBirth);
    }
}