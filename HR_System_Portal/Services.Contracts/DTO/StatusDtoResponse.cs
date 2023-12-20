using Domain.Entities.Entities;

namespace Services.Contracts.DTO
{
    public class StatusDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public JobReplySimpleDto JobReply { get; set; }
        public Guid JobReplyId { get; set; }
        public Guid StatusId { get; set; }
        public bool IsActual { get; set; }
        public bool IsEnded { get; set; }
    }
}
