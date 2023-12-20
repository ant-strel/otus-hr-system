using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Entities
{
    [Table("Statuses")]
    public class Status : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public Guid JobReplyId { get; set; }
        public virtual JobReply JobReply { get; set; }
        public bool IsEnded { get; set; }
        public bool IsActual { get; set; }
        public Guid StatusId { get; set; }
    }
}
