using Domain.Entities.Abstractions.Repositories;
using Domain.Entities.Entities;

using Services.Impl.Data;

namespace Infrastructure.EntityFramework
{
    public class MongoDbInitializer : IDbInitializer
    {
        private IRepository<Status> _statusesRepository;
        private IRepository<Command> _commandRepository;

        public MongoDbInitializer(
            IRepository<Status> statusesRepository, 
            IRepository<Command> commandRepository)
        {
            _statusesRepository = statusesRepository;
            _commandRepository = commandRepository;
        }

        public async void InitializeDb()
        {
            foreach (var entity in StartedWfInit.Statuses)
            {
                var temp = await _statusesRepository.GetByIdAsync(entity.Id);
                if (temp != null)
                    continue;
                await _statusesRepository.AddAsync(entity);
            }
            foreach (var entity in StartedWfInit.Command)
            {
                var temp = await _commandRepository.GetByIdAsync(entity.Id);
                if (temp != null)
                    continue;
                await _commandRepository.AddAsync(entity);
            }

        }
    }
}
