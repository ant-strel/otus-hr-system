using AutoMapper;
using Domain.Entities.Entities;
using HR_Portal_API.Models.Request;
using HR_Portal_API.Models.Response;
using Services.Contracts.DTO;

namespace HR_Portal_API.Mapping;

public class CandidateMappingsProfile : Profile
{
    public CandidateMappingsProfile()
    {
        CreateMap<CandidateFullDto, Candidate>().ReverseMap();

        CreateMap<CandidateFullDto, CandidateRequest>();

        CreateMap<CandidateFullDto, CandidateFullRequest>();

        CreateMap<CandidateRequest, CandidateFullDto>()
           .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.NewGuid()));

        CreateMap<CandidateFullRequest, CandidateFullDto>()
           .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id));

        CreateMap<CandidateFullDto, CandidateSimpleResponse>()
            .ForMember(x => x.FullName, opt => opt.MapFrom(x => this.GetFullName(x)));

        CreateMap<CandidateFullDto, CandidateFullResponse>();
    }

    private string GetFullName(CandidateFullDto dto)
    {
        return String.Join(" ", dto.FirstName, dto.LastName, dto.Surname);
    }
}
