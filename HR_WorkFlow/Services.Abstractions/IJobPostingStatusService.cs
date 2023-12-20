using Domain.Entities.DTO;
using Services.Contracts.DTO;

namespace Services.Abstractions
{
    public interface IJobPostingStatusService
    {
        public Task<IEnumerable<JobPostingStatusResponse>> GetAll();
        public Task<JobPostingStatusResponse> GetByRequest(JobPostingStatusRequest request);
        public Task<JobPostingStatusResponse> Update (JobPostingStatusRequest request);
        public Task<bool> Delete (Guid id);   

    }
}