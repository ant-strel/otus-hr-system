using Domain.Entities.Abstractions.Repositories;
using Domain.Entities.Entities;
using Services.Abstractions;
using Services.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impl
{
    public class CommandService : ICommandService
    {
        private IRepository<Command> _commandRepository;

        public CommandService(IRepository<Command> commandRepository) => _commandRepository = commandRepository;

        public async Task<Guid> Create(CommandCreateRequest request)
        {
            Command command = new Command()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                EndStatusId = request.EndStatusId,
                NeedResolution = request.NeedResolution,
                StartStatusId = request.StartStatusId,
            };

            await _commandRepository.AddAsync(command);
            return command.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            var command = await _commandRepository.GetByIdAsync(id);
            if (command == null) { return false; }

            await _commandRepository.DeleteAsync(command);  
            return true;
        }

        public async Task Update(CommandEditRequest request)
        {
            var command = await _commandRepository.GetByIdAsync(request.Id);
            if (command == null)
            { 
                throw new NullReferenceException(request.Id.ToString()); 
            }
            command.Name = request.Name;
            command.StartStatusId = request.StartStatusId;
            command.EndStatusId = request.EndStatusId;
            command.NeedResolution = request.NeedResolution;

            await _commandRepository.UpdateAsync(command);
        }

        public async Task<IEnumerable<CommandResponse>> GetAll()
        {
            return (await _commandRepository.GetAllAsync()).Select(x => new CommandResponse(x));
        }

        public async Task<CommandResponse> GetById(Guid id)
        {
            var command = await _commandRepository.GetByIdAsync(id);
            if (command == null)
            {
                throw new NullReferenceException(id.ToString());
            }
            return new CommandResponse(command);
        }
    }
}
