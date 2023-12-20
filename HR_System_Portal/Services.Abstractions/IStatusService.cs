
using Bus;
using Services.Contracts.DTO;

namespace Services.Abstractions
{
    public interface IStatusService
    {
        public Task<Guid> CreateOrUpdateJobReplyStatus(StatusDto message);
        public Task<IEnumerable<StatusDto>> GetAll();
        public Task<StatusDto> GetById(Guid id);
        public Task Update(StatusDto request);
        public Task<bool> Delete(Guid id);
        public Task<Guid> Create(StatusDto request);
    }
}
