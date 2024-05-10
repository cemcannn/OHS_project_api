using Microsoft.Extensions.DependencyInjection;
using OHS_program_api.Application.Hubs;
using OHS_program_api.SignalR.HubServices;


namespace OHS_program_api.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection collection)
        {
            collection.AddTransient<IAccidentHubService, AccidentHubService>();
            collection.AddTransient<IPersonnelHubService, PersonnelHubService>();
            collection.AddSignalR();
        }
    }
}
