using AutoMapper;
using FluentValidation;

using HR_Portal_API.Bus;
using HR_Portal_API.Models.Request;
using HR_Portal_API.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using Services.Abstractions;
using Services.Contracts.DTO;

namespace HR_Portal_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService service;
        private readonly IMapper mapper;
        private readonly IHubContext<SrHub, IFrontClient> hub;
        public CandidateController(ICandidateService candidateService, IMapper mapper,
        IHubContext<SrHub, IFrontClient> hub)
        {
            this.hub = hub;
            this.service = candidateService;
            this.mapper = mapper;
        }
        /// <summary>
        /// Check the user access.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult UserAccess()
        {
            return Ok("Hello User");
        }
        /// <summary>
        /// Check the admin access.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminAccess()
        {
            return Ok("Hello Admin");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok((await this.service.GetAll()).Select(x => this.mapper.Map<CandidateFullResponse>(x)));
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(this.mapper.Map<CandidateFullResponse>(await this.service.GetById(id)));
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveById(Guid id)
        {
            var removed = await this.service.RemoveById(id);
            if (removed)
                return Ok(removed);
            
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Create(CandidateRequest candidate, [FromServices] IValidator<CandidateRequest> validator)
        {
            try
            {
                await validator.ValidateAndThrowAsync(candidate);

                return Ok(await this.service.CreateAsync(this.mapper.Map<CandidateFullDto>(candidate)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(CandidateFullRequest candidate, [FromServices] IValidator<CandidateFullRequest> validator)
        {
            try
            {
                await validator.ValidateAndThrowAsync(candidate);

                return Ok(this.mapper.Map<CandidateFullResponse>(await this.service.UpdateAsync(this.mapper.Map<CandidateFullDto>(candidate))));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> CreateFakeCandidates()
        {
            try
            {
                var candidate1 = Guid.Parse("EA8363B8-653C-4B6D-B21C-77010442E5BE".ToLower());
                var candidate2 = Guid.Parse("8A621120-7775-4E20-B516-BE4ABB4D580A".ToLower());
                List<CandidateFullDto> candidates = new List<CandidateFullDto>()
                {
                    new CandidateFullDto()
                    {
                        Id = candidate1,
                        Address = "SomeStreet 1",
                        Age = 18,
                        FirstName = "Ivan",
                        LastName = "Ivanov",
                        Surname = "",                    
                    },
                    new CandidateFullDto()
                    {
                        Id = candidate2,
                        Address = "AnotherStreet 2",
                        Age = 71,
                        FirstName = "Petr",
                        LastName = "Petrov",
                        Surname = "",
                    }
                };

                foreach(var candidate in candidates)
                {
                    await service.CreateAsync(candidate);
                }

                return Ok(candidates.Select(x => x.Id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}