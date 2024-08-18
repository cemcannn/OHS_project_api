using Microsoft.AspNetCore.SignalR;
using OHS_program_api.Application.Abstractions.Hubs;
using OHS_program_api.SignalR.Hubs;

namespace OHS_program_api.SignalR.HubServices
{
    public class PersonnelHubService : IPersonnelHubService
    {
        readonly IHubContext<PersonnelHub> _hubContext;

        public PersonnelHubService(IHubContext<PersonnelHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task PersonnelAddedMessageAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.PersonnelAddedMessage, message);
        }
    }
}
