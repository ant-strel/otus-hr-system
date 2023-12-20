using Domain.Entities.Entities;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
    public class StatusRepository : EfRepository<Status>
    {
        public StatusRepository(DatabaseContext dataContext) : base(dataContext)
        {

        }

        public override async Task<IEnumerable<Status>> GetAllAsync()
        {
            var entities = await this.dbSet
                .Include(x => x.JobReply)
                .Where(x => !x.IsDeleted)
                .ToListAsync();

            return entities;
        }

        public override async Task<Status?> GetByIdAsync(Guid id)
        {
            var entities = await this.dbSet
                .Include(x => x.JobReply)
                .Where(x => !x.IsDeleted && x.Id == id)
                .ToListAsync();
            var result = entities.FirstOrDefault();
            return result;
        }
    }
}
