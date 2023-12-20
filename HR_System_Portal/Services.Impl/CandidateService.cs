using AutoMapper;

using Domain.Entities.Abstractions.Repositories;
using Domain.Entities.Entities;

using Services.Abstractions;
using Services.Contracts.DTO;
using System.Security.Cryptography;

namespace Services.Impl;
public class CandidateService : ICandidateService
{
    private readonly IMapper mapper;
    private readonly IRepository<Candidate> candidateRepository;

    public CandidateService(
        IRepository<Candidate> candidateRepository,
        IMapper mapper
        )
    {
        this.candidateRepository = candidateRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Получение списка кандидатов
    /// </summary>
    /// <returns>возвращает IEnumerable CandidateDto</returns>
    public async Task<IEnumerable<CandidateFullDto>> GetAll()
    {
        return (await this.candidateRepository.GetAllAsync()).Select(x => this.mapper.Map<CandidateFullDto>(x));
    }

    /// <summary>
    /// Получение кандидата по ID
    /// </summary>
    /// <param name="id">GUID кандидата</param>
    /// <returns>возвращает CandidateDto</returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<CandidateFullDto> GetById(Guid id)
    {
        Candidate? candidate = await this.candidateRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Candidate not found");
        return this.mapper.Map<CandidateFullDto>(candidate);
    }

    /// <summary>
    /// Удаление кандидата по ID
    /// </summary>
    /// <param name="id">GUID кандидата</param>
    /// <returns>Возвращает BOOL</returns>
    public async Task<bool> RemoveById(Guid id)
    {
        return await this.candidateRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Создание нового кандидата
    /// </summary>
    /// <param name="obj">Объект Candidate</param>
    /// <returns>Возвращает GUID созданного кандидата</returns>
    public async Task<Guid> CreateAsync(CandidateFullDto obj)
    {
        return await this.candidateRepository.AddAsync(this.mapper.Map<Candidate>(obj));
    }

    /// <summary>
    /// Обновление данных существующего кандидата
    /// </summary>
    /// <param name="obj">Объект Candidate</param>
    /// <returns>Возвращает обновлённый объект Candidate</returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<CandidateFullDto> UpdateAsync(CandidateFullDto obj)
    {
        Candidate? candidate = await this.candidateRepository.GetByIdAsync(obj.Id);

        if (candidate != null)
            await this.candidateRepository.UpdateAsync(this.mapper.Map<Candidate>(obj));
        else
            throw new KeyNotFoundException("Candidate not found");

        candidate = await this.candidateRepository.GetByIdAsync(obj.Id);

        return candidate == null 
            ? throw new KeyNotFoundException("Candidate not found") 
            : this.mapper.Map<CandidateFullDto>(candidate);
    }

}
