using System.Collections.Generic;
using System.Linq.Expressions;

using Domain.Entities.Abstractions.Repositories;
using Domain.Entities.Entities;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
    public class EfRepository<T>
        : IRepository<T>
        where T : BaseEntity
    {
        protected readonly DatabaseContext dataContext;
        protected readonly DbSet<T> dbSet;

        public EfRepository(DatabaseContext dataContext)
        {
            this.dataContext = dataContext;
            this.dbSet = dataContext.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetByExpression(Expression<Func<T, bool>> predicate)
        {
            return this.dbSet.Where(predicate);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await this.dbSet.Where(x => !x.IsDeleted).ToListAsync();

            return entities;
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            var entity = await this.dbSet.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            return entity;
        }

        public virtual async Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids)
        {
            var entities = await this.dbSet.Where(x => ids.Contains(x.Id) && !x.IsDeleted).ToListAsync();
            return entities;
        }

        public virtual async Task<Guid> AddAsync(T entity)
        {
            await this.dbSet.AddAsync(entity);

            await this.dataContext.SaveChangesAsync();

            return entity.Id;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            var local = dataContext.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entity.Id));

            if (local != null)
                dataContext.Entry(local).State = EntityState.Detached;

            dataContext.Entry(entity).State = EntityState.Modified;
            await this.dataContext.SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var obj = await this.dbSet.FirstOrDefaultAsync(x => x.Id == id);
            obj.IsDeleted = true;
            return (await dataContext.SaveChangesAsync()) > 0;
        }
    }
}
