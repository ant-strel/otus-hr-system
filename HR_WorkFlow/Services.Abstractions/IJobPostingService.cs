using Services.Contracts.DTO;

namespace Services.Abstractions
{
    public interface IJobPostingService
    {
        public Task<Guid> Create(Guid id);
        public Task<IEnumerable<JobPostingResponse>> GetAll();
        public Task<JobPostingResponse> GetById(Guid id);
        public Task<bool> Delete(Guid id);
        public Task<bool> DeleteAllPermanent();
    }
}