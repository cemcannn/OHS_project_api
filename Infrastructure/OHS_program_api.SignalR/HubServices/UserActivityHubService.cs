using Microsoft.AspNetCore.SignalR;
using OHS_program_api.Application.Abstractions.Hubs;
using OHS_program_api.SignalR.Hubs;

namespace OHS_program_api.SignalR.HubServices
{
    public class UserActivityHubService : IUserActivityHubService
    {
        readonly IHubContext<UserActivityHub> _hubContext;

        public UserActivityHubService(IHubContext<UserActivityHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendActivityAsync(string message)
            => await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.UserActivityMessage, message);
    }
}
