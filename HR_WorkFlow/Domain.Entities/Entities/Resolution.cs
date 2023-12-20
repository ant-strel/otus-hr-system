namespace Domain.Entities.Entities
{
    public class Resolution:BaseEntity
    {
        public Guid JobPostingId { get; set; }
        public virtual JobPosting JobPosting { get; set; }
        public Guid StatusId { get; set; }
        public virtual Status Status { get; set; }  
        public string Name { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
