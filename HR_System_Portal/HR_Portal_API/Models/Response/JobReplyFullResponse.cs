using HR_Portal_API.Models.Request;

namespace HR_Portal_API.Models.Response
{
    public class JobReplyFullResponse
    {
        public Guid Id { get; set; }

        public JobShortResponse Job { get; set; }

        public CandidateSimpleResponse Candidate { get; set; }

        public StatusResponse Status { get; set; }
    }

    public class JobReplySimpleResponse
    {
        public Guid Id { get; set; }

        public Guid JobId { get; set; }

        public Guid CandidateId { get; set; }

        public Guid StatusId { get; set; }
    }
}
