using Bus;
using MassTransit;
using Services.Abstractions;
using System.Diagnostics;

namespace HR_WorkFlow_API.Bus
{
    public class JobPostingConsumer : IConsumer<CreatingJobPostingDto>
    {
        private IJobPostingService _jobPostingService;
        public JobPostingConsumer(IJobPostingService jobPostingService) => _jobPostingService = jobPostingService;
        public async Task Consume(ConsumeContext<CreatingJobPostingDto> context)
        {
            try
            {
                await _jobPostingService.Create(context.Message.JobPostingId);
                Debug.WriteLine($"CreatingJobPostingDto received - {context.Message}");
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
