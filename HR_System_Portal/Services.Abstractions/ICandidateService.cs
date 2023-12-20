using Domain.Entities.Entities;
using Services.Contracts.DTO;
using System.Threading.Tasks;

namespace Services.Abstractions;
public interface ICandidateService
{
    Task<IEnumerable<CandidateFullDto>> GetAll();
    Task<CandidateFullDto> GetById(Guid id);
    Task<bool> RemoveById(Guid id);
    Task<Guid> CreateAsync(CandidateFullDto candidate);
    Task<CandidateFullDto> UpdateAsync(CandidateFullDto candidate);
}
