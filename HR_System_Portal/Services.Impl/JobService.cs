using AutoMapper;

using Domain.Entities.Abstractions.Repositories;
using Domain.Entities.Entities;

using Services.Abstractions;
using Services.Contracts.DTO;

namespace Services.Impl;
public class JobService : IJobService
{
    private readonly IMapper mapper;
    private readonly IRepository<Job> jobRepository;

    public JobService(
        IRepository<Job> jobRepository,
        IMapper mapper)
    {
        this.jobRepository = jobRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<JobDto>> GetAll()
    {
        return (await this.jobRepository.GetAllAsync()).Select(x => this.mapper.Map<JobDto>(x));
    }

    public async Task<JobDto> GetById(Guid id)
    {
        Job? job = await this.jobRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Job not found");
        return this.mapper.Map<JobDto>(job);
    }

    public async Task<bool> RemoveById(Guid id)
    {
        return await this.jobRepository.DeleteAsync(id);
    }

    public async Task<Guid> CreateAsync(JobDto obj)
    {
        return await this.jobRepository.AddAsync(this.mapper.Map<Job>(obj));
    }

    public async Task<JobDto> UpdateAsync(JobDto obj)
    {
        Job? job = await this.jobRepository.GetByIdAsync(obj.Id);

        if (job != null)
            await this.jobRepository.UpdateAsync(this.mapper.Map<Job>(obj));
        else
            throw new KeyNotFoundException("Job not found");

        job = await this.jobRepository.GetByIdAsync(obj.Id);

        return job == null
            ? throw new KeyNotFoundException("Job not found")
            : this.mapper.Map<JobDto>(job);
    }

}

