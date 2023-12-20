using AutoMapper;

using Domain.Entities.Entities;
using HR_Portal_API.Models.Request;
using HR_Portal_API.Models.Response;

using Services.Contracts.DTO;

namespace HR_Portal_API.Mapping
{
    public class StatusMappingProfile : Profile
    {
        public StatusMappingProfile()
        {
            CreateMap<StatusDto, Status>().ReverseMap();
            CreateMap<StatusDto, StatusResponse>();
        }
    }
}
