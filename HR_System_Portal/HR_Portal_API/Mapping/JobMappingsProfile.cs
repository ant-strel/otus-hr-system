using AutoMapper;
using Domain.Entities.Entities;

using HR_Portal_API.Models.Request;
using HR_Portal_API.Models.Response;

using Services.Contracts.DTO;

namespace HR_Portal_API.Mapping
{
    public class JobMappingsProfile : Profile
    {
        public JobMappingsProfile()
        {
            CreateMap<JobDto, Job>().ReverseMap();

            CreateMap<JobDto, JobRequest>();

            CreateMap<JobDto, JobFullRequest>();

            CreateMap<JobDto, JobShortResponse>();

            CreateMap<JobFullRequest, JobDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id));

            CreateMap<JobRequest, JobDto>()
               .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.NewGuid()));

            CreateMap<JobDto, JobFullResponse>();
        }
    }
}
