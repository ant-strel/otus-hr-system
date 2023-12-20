using Domain.Entities.Abstractions.Repositories;
using Domain.Entities.DTO;
using Domain.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.DTO;

namespace HR_WorkFlow_API.Controllers
{
    public class ResolutionController : Controller
    {
        private IResolutionService _resolutionService;

        public ResolutionController(IResolutionService resolutionService) => _resolutionService = resolutionService;

        /// <summary>
        /// Creates the resolution.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(ResolutionCreateRequest request)
        {
            return Ok(await _resolutionService.Create(request));

        }
        /// <summary>
        /// Gets the resolution by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ResolutionResponse>> GetById(Guid id)
        {
            return Ok(await _resolutionService.GetById(id));
        }
        /// <summary>
        /// Gets all resolutions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<ResolutionResponse>> GetAll()
        {
            return await _resolutionService.GetAll();  
        }
        /// <summary>
        /// Edits the resolution.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ResolutionResponse>> Edit(ResolutionEditRequest request)
        {
            return Ok(await _resolutionService.Update(request));
        }
        /// <summary>
        /// Deletes the resolution.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _resolutionService.Delete(id);
            return Ok();
        }
    }
}
