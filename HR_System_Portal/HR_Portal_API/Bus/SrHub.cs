using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace HR_Portal_API.Bus
{
    public interface IFrontClient
    {
        Task ReceiveMessageOnFront(string message);
    }

    public class SrHub : Microsoft.AspNetCore.SignalR.Hub<IFrontClient>
    {
        public async Task SendAsync(string message)
        {
            await Clients.All.ReceiveMessageOnFront(message);
        }
    }
}
