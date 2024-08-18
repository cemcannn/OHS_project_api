namespace OHS_program_api.Application.Abstractions.Hubs
{
    public interface IAccidentHubService
    {
        Task AccidentAddedMessageAsync(string message);
    }
}
