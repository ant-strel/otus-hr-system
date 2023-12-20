using Domain.Entities.Abstractions.Repositories;
using Domain.Entities.Entities;
using Services.Abstractions;
using Services.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impl
{
    public class ResolutionService : IResolutionService
    {
        private IRepository<Resolution> _resolutionRepository;

        public ResolutionService(IRepository<Resolution> resolutionRepository) => _resolutionRepository = resolutionRepository;
        public async Task<Guid> Create(ResolutionCreateRequest request)
        {
            Resolution resolution = new Resolution()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                StatusId = request.StatusId,
                DateTime = DateTime.Parse(request.DateTime),
                EmployeeId = request.EmployeeId,
                JobPostingId = request.JobPostingId
            };

            await _resolutionRepository.AddAsync(resolution);

            return resolution.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            var command = await _resolutionRepository.GetByIdAsync(id);
            if (command == null) 
            {
                throw new NullReferenceException($"{id} is null");
            }

            await _resolutionRepository.DeleteAsync(command);
            return true;
        }

        public async Task<IEnumerable<ResolutionResponse>> GetAll()
        {
            var commands = await _resolutionRepository.GetAllAsync();
            var result = commands.Select(x => new ResolutionResponse(x));
            return result;
        }

        public async Task<ResolutionResponse> GetById(Guid id)
        {
            var resolution = await _resolutionRepository.GetByIdAsync(id);
            if (resolution == null)
            {
                throw new NullReferenceException($"{id} is null");
            }
            return new ResolutionResponse(resolution);
        }

        public async Task<ResolutionResponse> Update(ResolutionEditRequest request)
        {
            var resolution = await _resolutionRepository.GetByIdAsync(request.Id);
            if (resolution == null)
            {
                throw new NullReferenceException($"{request.Id} is null");
            }
            resolution.Name = request.Name;
            resolution.StatusId = request.StatusId;
            resolution.DateTime = DateTime.Parse(request.DateTime);
            resolution.EmployeeId = request.EmployeeId;
            resolution.JobPostingId = request.JobPostingId;
            resolution.EmployeeId = request.EmployeeId;

            await _resolutionRepository.UpdateAsync(resolution);

            return new ResolutionResponse(resolution);
        }
    }
}
