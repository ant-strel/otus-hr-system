using Bus;
using Domain.Entities.Abstractions.Repositories;
using Domain.Entities.Entities;
using MassTransit;
using Services.Abstractions;
using Services.Contracts.DTO;

namespace Services.Impl
{
    public class StatusService : IStatusService
    {
        private IRepository<Status> _statusRepository;
        private IRepository<JobPostingStatus> _jobPostingStatusRepository;
        private IBusControl _busControl;
        private readonly string _initialAndfinalEqualMessage = "Status should be isInitial or isFinal";
        private readonly string _initialExistsMessage = "Is already exist initial status with id ";
        private readonly string _finalExistsMessage = "Is already exist final status with id ";
        public StatusService(IRepository<Status> statusRepository, IRepository<JobPostingStatus> jobPostingStatusRepository, IBusControl busControl)
        {
            _statusRepository = statusRepository;
            _jobPostingStatusRepository = jobPostingStatusRepository;
            _busControl = busControl;
        }

        public async Task<Guid> Create(StatusCreateRequest request)
        {
            if (request.IsInitial && request.IsFinal)
                throw new InvalidDataException(_initialAndfinalEqualMessage);

            var initialStatus = await GetInitialStatus();
            if (initialStatus != null)
            {
                if (request.IsInitial)
                    throw new InvalidDataException(_initialExistsMessage + initialStatus.Id);
            }
            var isFinalExists = await GetFinalStatus();
            if (isFinalExists != null)
            {
                if (request.IsFinal)
                    throw new InvalidDataException(_finalExistsMessage + isFinalExists.Id);
            }


            Status status = new Status()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                IsFinal = request.IsFinal,
                IsInitial = request.IsInitial,
            };

            await _statusRepository.AddAsync(status);

            SendBusMessage(status);

            return status.Id;
        }

        private async void SendBusMessage(Status status)
        {
            var jobpostingstatuses = await _jobPostingStatusRepository.GetAllAsync();
            var filtered = jobpostingstatuses.Where(s => s.StatusId == status.Id);

            if(filtered.Count() == 0)
                return;

            foreach (var jobpostingstatus in filtered)
            {
                await _busControl.Publish(new JobPostingStatusChangedDto()
                {
                    Id=jobpostingstatus.Id,
                    StatusId=jobpostingstatus.StatusId.ToString(),
                    ResponseId = jobpostingstatus.JobPostingId.ToString(),
                    IsEnded = status.IsFinal,
                    Name = status.Name,
                });
            }
        }

        private async Task<Status> GetFinalStatus()
        {
            var statuses = await _statusRepository.GetAllAsync();
            return statuses.FirstOrDefault(x => x.IsFinal);
        }

        private async Task<Status> GetInitialStatus()
        {
            var statuses = await _statusRepository.GetAllAsync();
            return statuses.FirstOrDefault(x => x.IsInitial);
        }
        public async Task<bool> Delete(Guid id)
        {
            var status = await _statusRepository.GetByIdAsync(id);
            if (status == null)
                throw new NullReferenceException($"{id} is null");

            await _statusRepository.DeleteAsync(status);
            return true;
        }

        public async Task<IEnumerable<StatusResponse>> GetAll()
        {
            var statuses = await _statusRepository.GetAllAsync();
            var result = statuses.Select(x => new StatusResponse(x));
            return result;
        }

        public async Task<StatusResponse> GetById(Guid id)
        {
            var status = await _statusRepository.GetByIdAsync(id);
            if (status == null)
            {
                throw new NullReferenceException($"{id} is null");
            }
            StatusResponse response = new StatusResponse(status);
            return response;
        }

        public async Task<StatusResponse> Update(StatusEditRequest request)
        {
            if (request.IsInitial && request.IsFinal)
                throw new InvalidDataException(_initialAndfinalEqualMessage);

            var status = await _statusRepository.GetByIdAsync(request.Id);
            if (status == null)
            {
                throw new NullReferenceException($"{request.Id} is null");
            }
            if (request.IsInitial)
            {
                var initial = await GetInitialStatus();
                if (initial != null && initial.Id != request.Id)
                    throw new InvalidDataException(_initialExistsMessage + initial.Id);
            }
            if (request.IsFinal)
            {
                var final = await GetFinalStatus();
                if (final != null && final.Id != request.Id)
                    throw new InvalidDataException(_finalExistsMessage + final.Id);
            }

            status.Name = request.Name;
            status.Description = request.Description;
            status.IsInitial = request.IsInitial;
            status.IsFinal = request.IsFinal;


            await _statusRepository.UpdateAsync(status);
            SendBusMessage(status);

            return new StatusResponse(status);
        }
    }
}
