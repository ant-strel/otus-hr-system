namespace Domain.Entities.Entities
{
    public class Status : BaseEntity
    {
        public string Name { get; set; }
        public bool IsInitial { get; set; }
        public bool IsFinal { get; set; }
        public string Description { get; set; }
        public virtual ICollection<JobPostingStatus> JobPostingStatuses { get; set; }
        public virtual ICollection<Command> Commands { get; set; }
        public virtual ICollection<Resolution> Resolutions { get; set; }

    }
}
