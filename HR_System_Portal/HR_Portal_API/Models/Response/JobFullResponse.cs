namespace HR_Portal_API.Models.Response
{
    public class JobFullResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual IEnumerable<JobReplyFullResponse> JobReplies { get; set; }
    }

    public class JobShortResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}