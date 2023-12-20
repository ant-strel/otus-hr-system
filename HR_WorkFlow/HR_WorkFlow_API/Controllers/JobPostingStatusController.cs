using Domain.Entities.Abstractions.Repositories;
using Domain.Entities.DTO;
using Domain.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.DTO;

namespace HR_WorkFlow_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class JobPostingStatusController : Controller
    {
        private IJobPostingStatusService _jobPostingStatusService;
        public JobPostingStatusController(IJobPostingStatusService jobPostingStatusService) => _jobPostingStatusService = jobPostingStatusService;
        
        /// <summary>
        /// Gets the job posting statuses.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IEnumerable<JobPostingStatusResponse>> GetAll()
        {
            return await _jobPostingStatusService.GetAll();
        }
        /// <summary>
        /// Gets the job posting status.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<JobPostingStatusResponse>> GetByRequest(JobPostingStatusRequest request)
        {
            return Ok(await _jobPostingStatusService.GetByRequest(request));
        }

        /// <summary>
        /// Updates the job posting status.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<JobPostingResponse>> Update(JobPostingStatusRequest request)
        {

            return Ok(await _jobPostingStatusService.Update(request));
        }
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id) 
        {
            await _jobPostingStatusService.Delete(id);
            return Ok();
        }
    }
}
