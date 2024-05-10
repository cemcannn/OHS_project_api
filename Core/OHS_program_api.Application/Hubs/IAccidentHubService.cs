namespace OHS_program_api.Application.Hubs
{
    public interface IAccidentHubService
    {
        Task AccidentAddedMessageAsync(string message);
    }
}
