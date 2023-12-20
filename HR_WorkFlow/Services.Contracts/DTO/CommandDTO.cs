using Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.DTO
{
    public class CommandResponse
    {
        public CommandResponse() { }
        public CommandResponse(Command command)
        {
            Id= command.Id;
            Name= command.Name;
            StartStatusId= command.StartStatusId;
            EndStatusId= command.EndStatusId;
            Name= command.Name;
            NeedResolution = command.NeedResolution;
        }
        public Guid Id { get; set; }
        public Guid StartStatusId { get; set; }
       
        public Guid EndStatusId { get; set; }

        public string Name { get; set; }
        public bool NeedResolution { get; set; }
    }
    public class CommandCreateRequest
    {
        [Required]
        public Guid StartStatusId { get; set; }
        [Required]
        public Guid EndStatusId { get; set; }
        [MaxLength(255)]
        [MinLength(1)]
        [NotNull]
        public string Name { get; set; }
        public bool NeedResolution { get; set; }
    }
    public class CommandEditRequest
    {
        [Required]
        public Guid Id { get; set; }
        public Guid StartStatusId { get; set; }
        public Guid EndStatusId { get; set; }
        [MaxLength(255)]
        [MinLength(1)]
        [NotNull]
        public string Name { get; set; }
        public bool NeedResolution { get; set; }
    }
}
