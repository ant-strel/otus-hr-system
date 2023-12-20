using System;
using System.Threading.Tasks;
using Domain.Entities.Entities;

namespace Services.Abstractions
{

public interface IJobService
{
    Task<Candidate> GetByGuid(Guid id);
}
}