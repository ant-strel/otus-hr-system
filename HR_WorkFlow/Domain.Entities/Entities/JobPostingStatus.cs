namespace Domain.Entities.Entities
{
    public class JobPostingStatus:BaseEntity
    {
        public JobPostingStatus() 
        { 

        }
        public JobPostingStatus(Guid responseId,Guid statusId)
        {
            JobPostingId = responseId;
            StatusId = statusId;
        }
        public Guid JobPostingId { get; set; }
        public virtual JobPosting JobPosting { get; set; }
        public Guid StatusId { get; set; }
        public virtual Status Status { get; set; }
    }
}
