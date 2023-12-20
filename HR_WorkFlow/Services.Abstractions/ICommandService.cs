using Services.Contracts.DTO;

namespace Services.Abstractions
{
    public interface ICommandService
    {
        public Task<Guid> Create(CommandCreateRequest request);

        public Task<CommandResponse> GetById(Guid id);
        public Task<IEnumerable<CommandResponse>> GetAll();

        public Task Update(CommandEditRequest request);
        public Task<bool> Delete(Guid id);

    }
}