using MediatR;

namespace OHS_program_api.Application.Features.Queries.AuditLog.GetAuditLogs
{
    public class GetAuditLogsQueryRequest : IRequest<GetAuditLogsQueryResponse>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
        public string? UserName { get; set; }
    }
}
