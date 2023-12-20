using Bus;
using Domain.Entities.Abstractions.Repositories;
using Domain.Entities.Entities;
using MassTransit;
using Services.Abstractions;
using Services.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impl
{
    public class JobPostingService : IJobPostingService
    {
        private IBusControl _busControl;
        private IRepository<Status> _statusRepository;
        private IRepository<JobPosting> _jobPostingRepository;
        private IRepository<JobPostingStatus> _jobPostingStatusRepository;

        public JobPostingService(IRepository<JobPosting> jobPostingRepository, 
            IRepository<JobPostingStatus> jobPostingStatusRepository, 
            IRepository<Status> statusRepository,
            IBusControl busControl)
        {
            _busControl = busControl;
            _statusRepository = statusRepository;
            _jobPostingRepository = jobPostingRepository;
            _jobPostingStatusRepository = jobPostingStatusRepository;
        }

        public async Task<Guid> Create(Guid guid)
        {
            JobPosting response = new JobPosting();
            response.Id = guid;

            var statuses = await _statusRepository.GetAllAsync();
            var status = statuses.FirstOrDefault(x => x.IsInitial);
            if (status == null)
                throw new NullReferenceException("Initial status is null");

            JobPostingStatus responseStatus = new JobPostingStatus();
            responseStatus.Id = Guid.NewGuid();
            responseStatus.Status = status;
            responseStatus.StatusId = status.Id;
            responseStatus.JobPosting = response;
            responseStatus.JobPostingId = guid;

            await _jobPostingRepository.AddAsync(response);
            await _jobPostingStatusRepository.AddAsync(responseStatus);
            await _busControl.Publish(new JobPostingStatusChangedDto()
            {
                Id = responseStatus.Id,
                IsEnded = status.IsFinal,
                Name = status.Name,
                ResponseId = responseStatus.JobPostingId.ToString(),
                StatusId = responseStatus.StatusId.ToString(),
            });
            return responseStatus.StatusId;
        }

        public async Task<bool> Delete(Guid id)
        {
            var jobposting = await _jobPostingRepository.GetByIdAsync(id);
            if (jobposting == null)
                throw new NullReferenceException();

            await _jobPostingRepository.DeleteAsync(jobposting);
            var jobPostingStatuses = await _jobPostingStatusRepository.GetAllAsync();
            var jobPostingStatus = jobPostingStatuses.FirstOrDefault(x => x.JobPostingId == id);
            if (jobPostingStatus != null)
                await _jobPostingStatusRepository.DeleteAsync(jobPostingStatus);

            return true;
        }
        
        public async Task<bool> DeleteAllPermanent()
        {
            var jobpostings = (await _jobPostingRepository.GetAllAsync()).ToList();
            var jobPostingStatuses = (await _jobPostingStatusRepository.GetAllAsync()).ToList();
            jobpostings.ForEach(async (jobposting)=> {
                var statuses = jobPostingStatuses.Where(x => x.JobPostingId == jobposting.Id).ToList();
                statuses.ForEach(async (status) => await _jobPostingStatusRepository.DeleteAsync(status));
                await _jobPostingRepository.DeleteAsync(jobposting);
                });
            return true;
        }

        public async Task<IEnumerable<JobPostingResponse>> GetAll()
        {
            var responses = await _jobPostingRepository.GetAllAsync();
            var responseObjects = responses.Select(x => new JobPostingResponse() { Id = x.Id });

            return responseObjects;
        }

        public async Task<JobPostingResponse> GetById(Guid id)
        {
            var result = await _jobPostingRepository.GetByIdAsync(id);
            if (result == null)
                throw new NullReferenceException($"{id} is null");

            return new JobPostingResponse { Id = result.Id  };
        }
    }
}
