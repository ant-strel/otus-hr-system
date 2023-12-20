using AutoMapper;

using Bus;
using Domain.Entities.Abstractions.Repositories;
using Domain.Entities.Entities;
using Services.Abstractions;
using Services.Contracts.DTO;
using System.Diagnostics;

namespace Services.Impl
{
    public class StatusService : IStatusService
    {
        private readonly IMapper mapper;
        private IRepository<Status> statusRepository;
        private IRepository<JobReply> jobReplyRepository;

        public StatusService(IRepository<Status> statusRepository,
            IRepository<JobReply> jobReplyRepository,
            IMapper mapper)
        {
            this.statusRepository = statusRepository;
            this.jobReplyRepository = jobReplyRepository;
            this.mapper = mapper;
        }
        public async Task<Guid> CreateOrUpdateJobReplyStatus(StatusDto request)
        {
            try
            {
                var status = await this.statusRepository.GetByIdAsync(request.Id);
                if (status == null)
                {
                    var jobReply = await this.jobReplyRepository.GetByIdAsync(request.JobReplyId);
                    if (jobReply == null)
                        throw new NullReferenceException($"jobreply {request.JobReplyId} is null");
                    status = new Status()
                    {
                        Id = request.Id,
                        IsDeleted = false,
                        IsEnded = request.IsEnded,
                        JobReply = jobReply,
                        Name = request.Name,
                        IsActual = true,
                    };
                    await this.statusRepository.AddAsync(status);
                    jobReply.IsActive = true;
                    jobReply.StatusId = status.Id;
                    await jobReplyRepository.UpdateAsync(jobReply);
                    return status.Id;
                }
                else
                {
                    status.IsEnded = request.IsEnded;
                    status.Name = request.Name;
                    status.IsActual = true;
                    await this.statusRepository.UpdateAsync(status);
                    return status.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<StatusDto>> GetAll()
        {
            var statuses = await statusRepository.GetAllAsync();

            return statuses.Select(x => this.mapper.Map<StatusDto>(x));
        }

        public async Task<StatusDto> GetById(Guid id)
        {
            var status = await this.statusRepository.GetByIdAsync(id);
            if (status == null)
                throw new NullReferenceException($"{id} is null");

            return new StatusDto()
            {
                Id = status.Id,
                JobReplyId = status.JobReply.Id,
                Name=status.Name,
                IsEnded=status.IsEnded,
            };
        }

        public async Task Update(StatusDto request)
        {
            var status = await statusRepository.GetByIdAsync(request.Id);
            if (status == null)
                throw new NullReferenceException($"{request.Id} is null");
            var jobReply =await jobReplyRepository.GetByIdAsync(request.JobReplyId);
            if (jobReply == null)
                throw new NullReferenceException($"jobreply {request.JobReplyId} is null");
            status.JobReply = jobReply;
            status.IsEnded = request.IsEnded;
            status.Name = request.Name;
            await statusRepository.UpdateAsync(status);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await statusRepository.DeleteAsync(id);
        }

        public async Task<Guid> Create(StatusDto request)
        {
            var status = await statusRepository.GetByIdAsync(request.Id);
            if (status != null) 
            {
               await Update(request);
                return status.Id;
            }
            var jobReply = await jobReplyRepository.GetByIdAsync(request.JobReplyId);
            if (jobReply == null)
                throw new NullReferenceException($"jobreply {request.JobReplyId} is null");

            status = new Status()
            {
                Id = Guid.NewGuid(),
                IsEnded = request.IsEnded,
                JobReply = jobReply,
                Name = request.Name
            };
            await statusRepository.AddAsync(status);
            return status.Id;
        }
    }
}
