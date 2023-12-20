
namespace Domain.Entities.DTO
{
    public class JobPostingStatusRequest
    {
        public Guid Id { get; set; }
        public Guid StatusId { get; set; }  
        public Guid JobPostingId { get; set; }       
    }

    public class JobPostingStatusResponse
    {
        public Guid Id { get; set; }
        public Guid StatusId { get; set; }
        public Guid JobPostingId { get; set; }

    }
}
