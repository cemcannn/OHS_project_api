namespace OHS_program_api.Application.Abstractions.Hubs
{
    public interface IUserActivityHubService
    {
        Task SendActivityAsync(string message);
    }
}
