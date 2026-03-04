using OHS_program_api.Application.Abstractions.Services;
using OHS_program_api.Domain.Entities;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Services
{
    public class AuditLogService : IAuditLogService
    {
        readonly OHSProgramAPIDbContext _context;

        public AuditLogService(OHSProgramAPIDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(string userId, string userName, string action, string requestType)
        {
            await _context.AuditLogs.AddAsync(new AuditLog
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                UserName = userName,
                Action = action,
                RequestType = requestType,
                Timestamp = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
        }
    }
}
