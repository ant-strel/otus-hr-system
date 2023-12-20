using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Entities
{
    [Table("Replies")]
    public class JobReply : BaseEntity
    {
        public Guid CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }

        public Guid JobId { get; set; }
        public virtual Job Job { get; set; }

        public Guid StatusId { get; set; }
        public virtual Status Status { get; set; }
        public bool IsActive { get; set; }
    }
}
