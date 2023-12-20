namespace HR_Portal_API.Models.Response
{
    public class CandidateSimpleResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public virtual IEnumerable<JobReplySimpleResponse> JobReplies { get; set; }
    }

    public class CandidateFullResponse
    {
        public Guid Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public virtual IEnumerable<JobReplyFullResponse> JobReplies { get; set; }
    }
}
