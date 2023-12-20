using Domain.Entities.Entities;

namespace Services.Contracts.DTO
{
    public class JobReplyFullDto
    {
        public Guid Id { get; set; }
        public CandidateFullDto Candidate { get; set; }
        public JobDto Job { get; set; }
        public StatusDto Status { get; set; }
    }

    public class JobReplySimpleDto
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public Guid JobId { get; set; }
        public Guid StatusId { get; set; }
    }
}
