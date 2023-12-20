using Services.Contracts.DTO;

namespace Services.Abstractions
{
    public interface IResolutionService
    {
        public Task<Guid> Create(ResolutionCreateRequest request);
        public Task<ResolutionResponse> GetById(Guid id);
        public Task<IEnumerable<ResolutionResponse>> GetAll();
        public Task<ResolutionResponse> Update(ResolutionEditRequest request);
        public Task<bool> Delete(Guid id);


    }
}