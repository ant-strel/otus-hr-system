using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.DTO;

namespace HR_Portal_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StatusController : ControllerBase
    {
        private IStatusService service;
        private IMapper mapper;

        public StatusController(IStatusService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await this.service.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await this.service.GetById(id));
        }

        //[HttpGet]
        //public async Task<IActionResult> RemoveById(Guid id)
        //{
        //    if (await this.service.GetById(id) != null)
        //    {
        //        return Ok(await this.service.Delete(id));
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(StatusDto job)
        //{
        //    try
        //    {
        //        return Ok(await this.service.Create(job));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

    }
}
