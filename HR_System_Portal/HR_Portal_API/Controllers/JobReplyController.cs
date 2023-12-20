using AutoMapper;

using HR_Portal_API.Models.Request;
using HR_Portal_API.Models.Response;

using Microsoft.AspNetCore.Mvc;

using Services.Abstractions;
using Services.Contracts.DTO;

namespace HR_Portal_API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class JobReplyController : ControllerBase
{
    private IJobReplyService service;
    private IMapper mapper;

    public JobReplyController(
        IJobReplyService service,
        IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok((await this.service.GetAll()).Select(x => this.mapper.Map<JobReplyFullResponse>(x)));
    }

    [HttpGet]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 60_0)] // 10 min
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(this.mapper.Map<JobReplyFullResponse>(await this.service.GetById(id)));
    }

    [HttpGet]
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

    [HttpPost]

    public async Task<IActionResult> Create(JobReplyRequest job)
    {
        try
        {
            return Ok(await this.service.CreateAsync(this.mapper.Map<JobReplySimpleDto>(job)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut]
    public async Task<ActionResult> CreateFakeJobReplies()
    {
        try
        {
            Guid job1 = Guid.Parse("A70B7732-791E-473F-B70B-538071FB6005".ToLower());
            var candidate1 = Guid.Parse("EA8363B8-653C-4B6D-B21C-77010442E5BE".ToLower());

            JobReplySimpleDto job = new JobReplySimpleDto()
            {
                JobId = job1,
                CandidateId = candidate1
            };

            await service.CreateAsync(job);


            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}

