using Services.Contracts.DTO;

namespace Services.Abstractions
{
    public interface IJobService
    {
        Task<IEnumerable<JobDto>> GetAll();
        Task<JobDto> GetById(Guid id);
        Task<bool> RemoveById(Guid id);
        Task<Guid> CreateAsync(JobDto job);
        Task<JobDto> UpdateAsync(JobDto job);
    }
}
