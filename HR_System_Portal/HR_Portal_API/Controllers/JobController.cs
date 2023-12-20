using AutoMapper;
using FluentValidation;
using HR_Portal_API.Models.Request;
using HR_Portal_API.Models.Response;

using Microsoft.AspNetCore.Mvc;

using Services.Abstractions;
using Services.Contracts.DTO;

namespace HR_Portal_API.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class JobController : ControllerBase
{
    private IJobService service;
    private IMapper mapper;

    public JobController(IJobService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok((await this.service.GetAll()).Select(x => this.mapper.Map<JobFullResponse>(x)));
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(this.mapper.Map<JobFullResponse>(await this.service.GetById(id)));
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveById(Guid id)
    {
        if (await this.service.GetById(id) != null)
        {
            return Ok(await this.service.RemoveById(id));
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPut]
    public async Task<IActionResult> Create(JobRequest job, [FromServices] IValidator<JobRequest> validator)
    {
        try
        {
            await validator.ValidateAndThrowAsync(job);

            return Ok(await this.service.CreateAsync(this.mapper.Map<JobDto>(job)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update(JobFullRequest job, [FromServices] IValidator<JobFullRequest> validator)
    {
        try
        {
            await validator.ValidateAndThrowAsync(job);

            return Ok(this.mapper.Map<JobFullResponse>(await this.service.UpdateAsync(this.mapper.Map<JobDto>(job))));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPut]
    public async Task<ActionResult> CreateFakeJobs()
    {
        try
        {
            Guid job1 = Guid.Parse("A70B7732-791E-473F-B70B-538071FB6005".ToLower());
            Guid job2 = Guid.Parse("522E82E3-3DA8-462C-AD86-A220327401D3".ToLower());


            List< JobDto> jobs = new List<JobDto>()
                {
                    new JobDto()
                    {
                        Id = job1,
                        Description = "Manager Description",
                        Name = "Manager",
                    },
                    new JobDto()
                    {
                        Id = job2,
                        Description = "Seller Description",
                        Name = "Seller"
                    }
                };

            foreach (var job in jobs)
            {
                await service.CreateAsync(job);
            }

            return Ok(jobs.Select(x => x.Id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}