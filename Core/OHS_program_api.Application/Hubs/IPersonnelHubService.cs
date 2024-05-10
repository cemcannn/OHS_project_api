namespace OHS_program_api.Application.Hubs
{
    public interface IPersonnelHubService
    {
        Task PersonnelAddedMessageAsync(string message);
    }
}
