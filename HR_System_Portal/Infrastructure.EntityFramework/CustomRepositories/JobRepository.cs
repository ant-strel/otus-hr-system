using Domain.Entities.Entities;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
    public class JobRepository : EfRepository<Job>
    {
        public JobRepository(DatabaseContext dataContext) : base(dataContext)
        {

        }

        public override async Task<IEnumerable<Job>> GetAllAsync()
        {
            var entities = await this.dbSet
                .Include(x => x.JobReplies)
                    .ThenInclude(x => x.Status)
                .Include(x => x.JobReplies)
                    .ThenInclude(x => x.Candidate)
                .Where(x => !x.IsDeleted)
                .ToListAsync();

            return entities;
        }

        public override async Task<Job?> GetByIdAsync(Guid id)
        {
            var entities = await this.dbSet
                .Include(x => x.JobReplies)
                    .ThenInclude(x => x.Status)
                .Include(x => x.JobReplies)
                    .ThenInclude(x => x.Candidate)
                .Where(x => !x.IsDeleted && x.Id == id)
                .ToListAsync();
            var result = entities.FirstOrDefault();
            return result;
        }
    }
}
