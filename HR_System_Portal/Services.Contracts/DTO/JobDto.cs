using Domain.Entities.Entities;

namespace Services.Contracts.DTO
{
    public class JobDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual IEnumerable<JobReplyFullDto>? JobReplies { get; set; }
    }
}
