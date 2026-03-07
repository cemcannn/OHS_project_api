using Microsoft.EntityFrameworkCore;
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

        public async Task<(List<AuditLog> logs, int total)> GetLogsAsync(int page, int size, string? userName = null)
        {
            var query = _context.AuditLogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(userName))
                query = query.Where(l => l.UserName.Contains(userName));

            var total = await query.CountAsync();
            var logs = await query
                .OrderByDescending(l => l.Timestamp)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return (logs, total);
        }
    }
}
