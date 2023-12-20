using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.DTO;

namespace HR_WorkFlow_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StatusController : Controller
    {
        private IStatusService _statusService;
        public StatusController(IStatusService statusService) => _statusService = statusService;
        /// <summary>
        /// Creates the Status.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(StatusCreateRequest request)
        {
            return Ok(await _statusService.Create(request));          
        }

        /// <summary>
        /// Gets Status by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<StatusResponse>> GetById(Guid id)
        {
            return Ok(await _statusService.GetById(id));
        }
        /// <summary>
        /// Gets all statuses.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<StatusResponse>> GetAll()
        {
            return await _statusService.GetAll();
        }
        /// <summary>
        /// Edits the command.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<StatusResponse>> Update(StatusEditRequest request)
        {
            return Ok(_statusService.Update(request));            
        }
        /// <summary>
        /// Deletes the command.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _statusService.Delete(id);
            return Ok();
        }

    }
}
