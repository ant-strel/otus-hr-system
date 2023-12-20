using Domain.Entities.Entities;

namespace Services.Contracts.DTO
{
    public class ResolutionCreateRequest
    {
        public string Name { get; set; }
        public Guid StatusId { get; set; }
        public string DateTime { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid JobPostingId { get; set; }
    }
    public class ResolutionEditRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid StatusId { get; set; }
        public string DateTime { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid JobPostingId { get; set; }
    }

    public class ResolutionResponse
    {
        public ResolutionResponse() { }
        public ResolutionResponse(Resolution resolution) 
        {
            Id = resolution.Id;
            StatusId = resolution.StatusId;
            DateTime = resolution.DateTime.ToString();
            EmployeeId = resolution.EmployeeId;
            JobPostingId= resolution.JobPostingId;
            Name = resolution.Name;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid StatusId { get; set; }
        public string DateTime { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid JobPostingId { get; set; }
    }
}
