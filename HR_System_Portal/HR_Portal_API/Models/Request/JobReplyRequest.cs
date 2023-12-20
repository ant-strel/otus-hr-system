namespace HR_Portal_API.Models.Request
{
    public record JobReplyRequest(
        string JobId, 
        string CandidateId);
}
