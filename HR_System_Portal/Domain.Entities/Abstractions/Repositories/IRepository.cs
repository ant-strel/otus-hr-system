using System.Collections.Generic;
using System.Linq.Expressions;

using Domain.Entities.Entities;

namespace Domain.Entities.Abstractions.Repositories
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        Task<IEnumerable<T>> GetByExpression(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids);

        Task<Guid> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task<bool> DeleteAsync(Guid id);
    }
}
