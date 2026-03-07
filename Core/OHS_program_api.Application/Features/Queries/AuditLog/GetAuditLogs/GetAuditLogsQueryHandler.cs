using MediatR;
using OHS_program_api.Application.Abstractions.Services;

namespace OHS_program_api.Application.Features.Queries.AuditLog.GetAuditLogs
{
    public class GetAuditLogsQueryHandler : IRequestHandler<GetAuditLogsQueryRequest, GetAuditLogsQueryResponse>
    {
        readonly IAuditLogService _auditLogService;

        public GetAuditLogsQueryHandler(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        public async Task<GetAuditLogsQueryResponse> Handle(GetAuditLogsQueryRequest request, CancellationToken cancellationToken)
        {
            var (logs, total) = await _auditLogService.GetLogsAsync(request.Page, request.Size, request.UserName);

            return new()
            {
                TotalCount = total,
                Logs = logs.Select(l => new AuditLogDto
                {
                    Id = l.Id,
                    UserName = l.UserName,
                    Action = l.Action,
                    RequestType = l.RequestType,
                    Timestamp = l.Timestamp
                }).ToList()
            };
        }
    }
}
