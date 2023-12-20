
namespace Bus
{
    public class JobPostingStatusChangedDto
    {
        public Guid Id { get; set; }
        public string ResponseId { get; set; }
        public string StatusId { get; set; }
        public string Name { get; set; }
        public bool IsEnded { get; set; }
    }
}
