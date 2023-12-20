namespace Domain.Entities.Entities
{
    public class JobPosting:BaseEntity
    {
        public JobPosting() { }   
        public JobPosting(Guid id)
        {
            Id = id;
        }
    }
}
