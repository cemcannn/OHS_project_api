namespace OHS_program_api.Application.Abstractions.Services
{
    public interface IAuditLogService
    {
        Task LogAsync(string userId, string userName, string action, string requestType);
    }
}
