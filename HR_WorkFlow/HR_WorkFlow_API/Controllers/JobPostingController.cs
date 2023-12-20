using Domain.Entities.Abstractions.Repositories;
using Domain.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.DTO;

namespace HR_WorkFlow_API.Controllers
{
    [ApiController]
    //[Authorize(Roles = "Administrator,User")]
    [Route("api/[controller]/[action]")]
    public class JobPostingController : Controller
    {
        private IJobPostingService _jobPostingService;

        public JobPostingController(IJobPostingService jobPostingService)
        {
            _jobPostingService = jobPostingService;
        }

        /// <summary>
        /// Creates the job posting.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(Guid guid)
        {
           return await _jobPostingService.Create(guid);                
        }

        /// <summary>
        /// Gets the job postings asynchronous.
        /// </summary>
        /// <returns></returns>        
        [HttpGet]       
        public async Task<IEnumerable<JobPostingResponse>> GetAll()
        {
            return await _jobPostingService.GetAll();
        }

        /// <summary>
        /// Gets the Job posting  by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<JobPostingResponse>> GetById(Guid id)
        {
            return Ok(await _jobPostingService.GetById(id));
        }

        /// <summary>
        /// Deletes the job posting.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>        
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await _jobPostingService.Delete(id))
                return Ok();
            else
                return BadRequest();
        }

        /// <summary>
        /// Deletes all the job posting.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>        
        [HttpDelete]
        public async Task<IActionResult> DeleteAllPermanent()
        {
            if (await _jobPostingService.DeleteAllPermanent())
                return Ok();
            else
                return BadRequest();
        }
    }
}
