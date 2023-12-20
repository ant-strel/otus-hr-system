using Domain.Entities.Abstractions.Repositories;
using Domain.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.DTO;

namespace HR_WorkFlow_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CommandController:Controller
    {
        private ICommandService _commandService;

        public CommandController(ICommandService commandService) => _commandService = commandService;
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
        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CommandCreateRequest request)
        {
            return Ok(await _commandService.Create(request));
        }
        /// <summary>
        /// Gets the command by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<CommandResponse>> GetById(Guid id)
        {
            return Ok(await _commandService.GetById(id));
        }
        /// <summary>
        /// Gets all commands.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<CommandResponse>> GetAll()
        {
            return await _commandService.GetAll();
        }
        /// <summary>
        /// Update the command.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> Update(CommandEditRequest request)
        {            
            return Ok(_commandService.Update(request)); 
        }
        /// <summary>
        /// Deletes the command.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (await _commandService.Delete(id))
                return Ok();
            else
                return BadRequest();
        }
    }
}
