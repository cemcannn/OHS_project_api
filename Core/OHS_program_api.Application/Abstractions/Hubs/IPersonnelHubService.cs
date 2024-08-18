namespace OHS_program_api.Application.Abstractions.Hubs
{
    public interface IPersonnelHubService
    {
        Task PersonnelAddedMessageAsync(string message);
    }
}
