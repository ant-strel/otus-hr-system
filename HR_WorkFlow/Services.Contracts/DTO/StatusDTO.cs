using Domain.Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace Services.Contracts.DTO
{
    public class StatusCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInitial { get; set; }
        public bool IsFinal { get; set; }
    }
    public class StatusEditRequest
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInitial { get; set; }
        public bool IsFinal { get; set; }
    }
    public class StatusResponse
    {
        public StatusResponse() { }
        public StatusResponse(Status status)
        {
            Id = status.Id;
            Name = status.Name;
            Description = status.Description;
            IsInitial = status.IsInitial;
            IsFinal = status.IsFinal;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInitial { get; set; }
        public bool IsFinal { get; set; }
    }
}