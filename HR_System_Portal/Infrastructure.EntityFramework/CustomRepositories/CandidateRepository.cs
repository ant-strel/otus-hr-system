using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities.Entities;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.CustomRepositories
{
    public class CandidateRepository : EfRepository<Candidate>
    {
        public CandidateRepository(DatabaseContext dataContext) : base(dataContext)
        {

        }
        public override async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            var entities = await this.dbSet
                .Include(x => x.JobReplies)
                .ThenInclude(x=>x.Job)
                .Include(x => x.JobReplies)
                .ThenInclude(x => x.Status)
                .Where(x => !x.IsDeleted)
                .ToListAsync();

            return entities;
        }

        public override async Task<Candidate?> GetByIdAsync(Guid id)
        {
            var entities = await this.dbSet
                .Include(x => x.JobReplies)
                .ThenInclude(x => x.Job)
                .Include(x => x.JobReplies)
                .ThenInclude(x => x.Status)
                .Where(x => !x.IsDeleted && x.Id == id)
                .ToListAsync();
            var result = entities.FirstOrDefault();
            return result;
        }
    }
}
