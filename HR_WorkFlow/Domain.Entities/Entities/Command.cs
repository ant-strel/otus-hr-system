namespace Domain.Entities.Entities
{
    public class Command:BaseEntity
    {
        public Guid StartStatusId { get; set; }
        public virtual Status StartStatus { get; set; }
        public Guid EndStatusId { get; set; }
        public virtual Status EndStatus { get; set; }
        public string Name { get; set; }
        public bool NeedResolution { get; set; }
    }
}
