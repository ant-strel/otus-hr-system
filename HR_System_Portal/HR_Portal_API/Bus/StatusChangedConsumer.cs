using Bus;
using MassTransit;

using Microsoft.AspNetCore.SignalR;

using Services.Abstractions;
using Services.Contracts.DTO;
using System.Diagnostics;

namespace HR_Portal_API.Bus
{
    public class StatusChangedConsumer:IConsumer<JobPostingStatusChangedDto>
    {
        private IStatusService statusService;
        private readonly IHubContext<SrHub, IFrontClient> hub;
        public StatusChangedConsumer(IStatusService statusesService,
        IHubContext<SrHub, IFrontClient> hub)
        {
            this.hub = hub;
            this.statusService = statusesService;
        }
        public async Task Consume(ConsumeContext<JobPostingStatusChangedDto> context)
        {
            Debug.WriteLine($"JobPostingStatusChangedDto received - {context.Message}");
            StatusDto statusDto = new StatusDto()
            {
                StatusId = Guid.Parse(context.Message.StatusId),
                Id = context.Message.Id,
                IsEnded = context.Message.IsEnded,
                JobReplyId = Guid.Parse(context.Message.ResponseId),
                Name = context.Message.Name,
            };

            var sendToFront = new SrHub();
            if (this.hub.Clients != null)
            {
                await this.hub.Clients.All.ReceiveMessageOnFront("Статус из WF обновился на " + context.Message.Name);
            }

            await statusService.CreateOrUpdateJobReplyStatus(statusDto);
        }
    }
}
