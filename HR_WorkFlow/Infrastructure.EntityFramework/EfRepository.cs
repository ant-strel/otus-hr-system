using Domain.Entities;
using Domain.Entities.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity

    {
        private readonly DataBaseContext _dataContext;
        private readonly DbSet<T> _dbSet;
        public EfRepository(DataBaseContext workFlowDbContext)
        {
            _dataContext = workFlowDbContext;
            _dbSet = _dataContext.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            var oldvalue = await GetByIdAsync(entity.Id);
            if (oldvalue == null)
                await _dataContext.Set<T>().AddAsync(entity);
            else
                oldvalue = entity;

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            _dataContext.Update(entity);
            //_dataContext.Set<T>().Remove(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dbSet.Where(x => !x.IsDeleted).ToListAsync();
            return entities;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            return entity;
        }


        public async Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids)
        {
            var entities = await _dbSet.Where(x => ids.Contains(x.Id) && !x.IsDeleted).ToListAsync();
            return entities;
        }

        public async Task UpdateAsync(T entity)
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
