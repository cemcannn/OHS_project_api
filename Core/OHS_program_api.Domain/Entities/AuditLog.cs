namespace OHS_program_api.Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string RequestType { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
