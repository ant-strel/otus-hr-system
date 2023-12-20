using Services.Contracts.DTO;

namespace Services.Abstractions
{
    public interface IJobReplyService
    {
        Task<IEnumerable<JobReplyFullDto>> GetAll();
        Task<JobReplyFullDto> GetById(Guid id);
        Task<bool> RemoveById(Guid id);
        Task<Guid> CreateAsync(JobReplySimpleDto obj);
    }
}
