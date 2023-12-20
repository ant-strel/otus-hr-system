using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Entities
{
    [Table("Candidates")]
    public class Candidate : BaseEntity
    {
        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Address { get; set; }

        public virtual IEnumerable<JobReply> JobReplies { get; set; }
    }
}
