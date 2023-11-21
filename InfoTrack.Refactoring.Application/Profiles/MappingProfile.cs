using AutoMapper;
using InfoTrack.Refactoring.Application.Features.Candidates.Commands.CreateCandidate;
using InfoTrack.Refactoring.Application.Features.Candidates.Queries.GetCandidateDetail;
using InfoTrack.Refactoring.Application.Features.Candidates.Queries.GetCandidatesList;
using InfoTrack.Refactoring.Application.Features.Positions.Commands.CreatePosition;
using InfoTrack.Refactoring.Domain.Entities;

namespace InfoTrack.Refactoring.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Candidate, CreateCandidateCommand>().ReverseMap();
            CreateMap<Candidate, CreateCandidateDto>();
            CreateMap<Candidate, CandidateListVm>();
            CreateMap<Candidate, CandidateDetailsVm>().ReverseMap();

            CreateMap<Position, CreatePositionCommand>().ReverseMap();
            CreateMap<Position, CreatePositionDto>().ReverseMap();
        }
    }
}
