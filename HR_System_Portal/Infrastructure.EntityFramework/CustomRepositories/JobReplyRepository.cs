using Domain.Entities.Entities;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
    public class JobReplyRepository : EfRepository<JobReply>
    {
        public JobReplyRepository(DatabaseContext dataContext) : base(dataContext)
        {

        }

        public override async Task<IEnumerable<JobReply>> GetAllAsync()
        {
            var entities = await this.dbSet
                .Include(x=>x.Candidate)
                .Include(x=>x.Job)
                .Include(x=>x.Status)
                .Where(x => !x.IsDeleted)
                .ToListAsync();

            return entities;
        }

        public override async Task<JobReply?> GetByIdAsync(Guid id)
        {
            var entities = await this.dbSet
                .Include(x=>x.Candidate)
                .Include(x=>x.Job)
                .Include(x=>x.Status)
                .Where(x => !x.IsDeleted && x.Id == id)
                .ToListAsync();
            var result = entities.FirstOrDefault();
            return result;
        }
    }
}
