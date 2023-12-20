using Bus;
using Domain.Entities.Abstractions.Repositories;
using Domain.Entities.DTO;
using Domain.Entities.Entities;
using MassTransit;
using Services.Abstractions;


namespace Services.Impl
{
    public class JobPostingStatusService : IJobPostingStatusService
    {
        private IRepository<JobPostingStatus> _jobPostingStatusRepository;
        private IRepository<Status> _statusRepository;
        private IBusControl _busControl;

        public JobPostingStatusService( IRepository<JobPostingStatus> jobPostingStatusRepository, IRepository<Status> statusRepository,  IBusControl busControl)
        {
            _jobPostingStatusRepository = jobPostingStatusRepository;
            _statusRepository = statusRepository;
            _busControl = busControl;
        }
        public async Task<bool> Delete(Guid id)
        {
            var result = await _jobPostingStatusRepository.GetByIdAsync(id);
            if (result == null)
            {
                throw new NullReferenceException($"{id} is null");
            }

            await _jobPostingStatusRepository.DeleteAsync(result);
            return true;
        }

        public async Task<IEnumerable<JobPostingStatusResponse>> GetAll()
        {
            var responseStatuses = await _jobPostingStatusRepository.GetAllAsync();
            var result = responseStatuses.Select(x => new JobPostingStatusResponse() { Id = x.Id, JobPostingId = x.JobPostingId, StatusId = x.StatusId });

            return result;
        }

        public async Task<JobPostingStatusResponse> GetByRequest(JobPostingStatusRequest request)
        {
            var result = await _jobPostingStatusRepository.GetByIdAsync(request.Id);

            if (result != null)
            {
                return new JobPostingStatusResponse() { Id = result.Id,  JobPostingId = result.JobPostingId, StatusId = result.StatusId};
            }

            var responseStatuses = await _jobPostingStatusRepository.GetAllAsync();

            result = responseStatuses.FirstOrDefault(x => x.StatusId == request.StatusId && x.JobPostingId == request.JobPostingId);

            if (result != null)
            {
                return new JobPostingStatusResponse() { Id = result.Id, JobPostingId = result.JobPostingId, StatusId = result.StatusId };
            }

            throw new NullReferenceException($"{request.Id} is null");
        }

        public async Task<JobPostingStatusResponse> Update(JobPostingStatusRequest request)
        {
            var result = await _jobPostingStatusRepository.GetByIdAsync(request.Id);

            if (result == null)
                throw new NullReferenceException($"jobpostingstatus {request.Id} is null");

            var status = await _statusRepository.GetByIdAsync(result.StatusId);
            if (status == null)
                throw new NullReferenceException($"status {result.StatusId} is null");

            result.StatusId = request.StatusId;

            await _jobPostingStatusRepository.UpdateAsync(result);

            await _busControl.Publish(new JobPostingStatusChangedDto()
            {
                Id = result.Id,
                IsEnded = status.IsFinal,
                Name = status.Name,
                ResponseId = result.JobPostingId.ToString(),
                StatusId = result.StatusId.ToString(),
            });

            return new JobPostingStatusResponse() { Id = result.Id, JobPostingId = result.JobPostingId, StatusId = result.StatusId };
        }
    }
}
