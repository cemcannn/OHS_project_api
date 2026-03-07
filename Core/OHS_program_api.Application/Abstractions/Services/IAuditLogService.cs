using OHS_program_api.Domain.Entities;

namespace OHS_program_api.Application.Abstractions.Services
{
    public interface IAuditLogService
    {
        Task LogAsync(string userId, string userName, string action, string requestType);
        Task<(List<AuditLog> logs, int total)> GetLogsAsync(int page, int size, string? userName = null);
    }
}
