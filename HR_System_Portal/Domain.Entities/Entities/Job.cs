using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Entities
{
    [Table("Jobs")]
    public class Job : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual IEnumerable<JobReply> JobReplies { get; set; }
    }
}
