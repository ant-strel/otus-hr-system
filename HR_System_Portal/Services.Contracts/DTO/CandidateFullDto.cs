using Domain.Entities.Entities;

namespace Services.Contracts.DTO
{
    public class CandidateFullDto
    {
        public Guid Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public virtual IEnumerable<JobReplyFullDto>? JobReplies { get; set; }
    }
}
