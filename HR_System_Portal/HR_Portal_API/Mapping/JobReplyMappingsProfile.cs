using AutoMapper;

using Domain.Entities.Entities;

using HR_Portal_API.Models.Request;
using HR_Portal_API.Models.Response;

using Services.Contracts.DTO;

namespace HR_Portal_API.Mapping
{
    public class JobReplyMappingsProfile : Profile
    {
        public JobReplyMappingsProfile()
        {
            CreateMap<JobReply, JobReplyFullDto>();
            CreateMap<JobReply, JobReplySimpleDto>();

            CreateMap<JobReplyFullDto, JobReplyFullResponse>();

            CreateMap<JobReplySimpleDto, JobReplySimpleResponse>();

            CreateMap<JobReplyFullDto, JobReplySimpleResponse>()
                .ForMember(x => x.StatusId, opt => opt.MapFrom(x => x.Status.Id))
                .ForMember(x => x.CandidateId, opt => opt.MapFrom(x => x.Candidate.Id))
                .ForMember(x => x.JobId, opt => opt.MapFrom(x => x.Job.Id));

            CreateMap<JobReplyRequest, JobReplySimpleDto>();
        }
    }
}
