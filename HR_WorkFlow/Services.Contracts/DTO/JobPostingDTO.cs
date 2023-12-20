namespace Services.Contracts.DTO
{
    public class JobPostingRequest
    {
        public Guid Id { get; set; }
        public Guid StatusId { get; set; }  
        public Guid JobPostingId { get; set; }       

    }
    public class JobPostingResponse
    {
        public Guid Id { get; set; }
    }
}
