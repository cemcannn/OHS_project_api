namespace OHS_program_api.Application.Features.Queries.AuditLog.GetAuditLogs
{
    public class GetAuditLogsQueryResponse
    {
        public List<AuditLogDto> Logs { get; set; }
        public int TotalCount { get; set; }
    }

    public class AuditLogDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string RequestType { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
