using Microsoft.AspNetCore.SignalR;
using OHS_program_api.Application.Hubs;
using OHS_program_api.SignalR.Hubs;

namespace OHS_program_api.SignalR.HubServices
{
    public class AccidentHubService : IAccidentHubService
    {
        readonly IHubContext<AccidentHub> _hubContext;

        public AccidentHubService(IHubContext<AccidentHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task AccidentAddedMessageAsync(string message)
            => await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.AccidentAddedMessage, message);
    }
}
