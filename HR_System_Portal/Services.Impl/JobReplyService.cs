using AutoMapper;
using Bus;
using Domain.Entities.Abstractions.Repositories;
using Domain.Entities.Entities;
using MassTransit;
using Services.Abstractions;
using Services.Contracts.DTO;

namespace Services.Impl
{
    public class JobReplyService : IJobReplyService
    {
        private readonly IMapper mapper;
        private readonly IBusControl busControl;
        private readonly IRepository<Status> statusRepository;
        private readonly IRepository<JobReply> jobReplyRepository;
        private readonly IRepository<Job> jobRepository;
        private readonly IRepository<Candidate> candidateRepository;

        public JobReplyService(
            IRepository<JobReply> jobReplyRepository,
            IRepository<Job> jobRepository,
            IRepository<Candidate> candidateRepository,
            IRepository<Status> statusRepository,
            IMapper mapper,
            IBusControl busControl
            )
        {
            this.statusRepository = statusRepository;
            this.jobReplyRepository = jobReplyRepository;
            this.jobRepository = jobRepository;
            this.candidateRepository = candidateRepository;
            this.mapper = mapper;
            this.busControl = busControl;
        }

        public async Task<Guid> CreateAsync(JobReplySimpleDto obj)
        {

            var candidate = await candidateRepository.GetByIdAsync(obj.CandidateId);
            if (candidate == null)
                throw new NullReferenceException($"candidate {obj.CandidateId} is null");
            var job = await jobRepository.GetByIdAsync(obj.JobId);
            if (job == null)
                throw new NullReferenceException($"job {obj.JobId} is null");


            JobReply jobReply = new JobReply()
            {
                Candidate = candidate,
                Job = job,
                Id = Guid.NewGuid(),
                IsDeleted = false,
                IsActive = false,
            };
            await jobReplyRepository.AddAsync(jobReply);

            await busControl.Publish(new CreatingJobPostingDto { JobPostingId = jobReply.Id });

            return jobReply.Id;
        }

        public async Task<IEnumerable<JobReplyFullDto>> GetAll()
        {
            return (await jobReplyRepository.GetAllAsync()).Select(x => mapper.Map<JobReplyFullDto>(x));
        }

        public async Task<JobReplyFullDto> GetById(Guid id)
        {
            JobReply? jobReply = await this.jobReplyRepository.GetByIdAsync(id);

            if (jobReply == null)
            {
                throw new KeyNotFoundException("Request not found");
            }

            return mapper.Map<JobReplyFullDto>(jobReply);
        }

        public async Task<bool> RemoveById(Guid id)
        {
            return await this.jobReplyRepository.DeleteAsync(id);
        }
    }
}
