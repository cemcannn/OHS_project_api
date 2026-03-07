using MediatR;
using Microsoft.AspNetCore.Http;
using OHS_program_api.Application.Abstractions.Hubs;
using OHS_program_api.Application.Abstractions.Services;
using System.Reflection;
using System.Security.Claims;

namespace OHS_program_api.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IUserActivityHubService _hubService;
        readonly IAuditLogService _auditLogService;

        public LoggingBehavior(
            IHttpContextAccessor httpContextAccessor,
            IUserActivityHubService hubService,
            IAuditLogService auditLogService)
        {
            _httpContextAccessor = httpContextAccessor;
            _hubService = hubService;
            _auditLogService = auditLogService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            var requestName = typeof(TRequest).Name;
            if (!requestName.Contains("Command"))
                return response;

            var claims = _httpContextAccessor.HttpContext?.User;

            // Kimliği doğrulanmamış istekleri loglama (Login, RefreshToken vb.)
            if (claims?.Identity?.IsAuthenticated != true)
                return response;

            var userId = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? claims.FindFirst("nameid")?.Value
                ?? claims.FindFirst("sub")?.Value
                ?? string.Empty;
            var userName = claims.FindFirst("display_name")?.Value
                ?? claims.FindFirst(ClaimTypes.Name)?.Value
                ?? claims.FindFirst("name")?.Value
                ?? claims.Identity?.Name
                ?? "Bilinmeyen";

            var action = BuildMessage(userName, requestName, request);

            try
            {
                await _auditLogService.LogAsync(userId, userName, action, requestName);
                await _hubService.SendActivityAsync(action);
            }
            catch { /* Loglama hatası ana işlemi engellemez */ }

            return response;
        }

        private static string BuildMessage(string userName, string requestTypeName, object request)
        {
            var entity = requestTypeName switch
            {
                var s when s.Contains("Accident") && !s.Contains("Statistic") && !s.Contains("Area") => "kaza",
                var s when s.Contains("AccidentStatistic") || s.Contains("Statistic") => "istatistik",
                var s when s.Contains("AccidentArea") || s.Contains("Area") => "kaza bölgesi",
                var s when s.Contains("Personnel") => "personel",
                var s when s.Contains("TypeOfAccident") => "kaza tipi",
                var s when s.Contains("Limb") => "uzuv",
                var s when s.Contains("Profession") => "meslek",
                var s when s.Contains("Directorate") => "müdürlük",
                var s when s.Contains("Role") && !s.Contains("User") => "rol",
                var s when s.Contains("User") || s.Contains("Password") || s.Contains("ProfilePhoto") => "kullanıcı",
                _ => "kayıt"
            };

            var verb = requestTypeName switch
            {
                var s when s.StartsWith("Create") || s.StartsWith("Add") => "ekledi",
                var s when s.StartsWith("Update") => "güncelledi",
                var s when s.StartsWith("Delete") || s.StartsWith("Remove") => "sildi",
                var s when s.StartsWith("Upload") => "yükledi",
                var s when s.StartsWith("Assign") => "atadı",
                _ => "güncelledi"
            };

            var details = ExtractDetails(request);
            return string.IsNullOrEmpty(details)
                ? $"{userName} {entity} {verb}"
                : $"{userName} {entity} {verb} — {details}";
        }

        private static string ExtractDetails(object request)
        {
            var props = request.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            string GetStr(string name) =>
                props.FirstOrDefault(p => p.Name == name)?.GetValue(request)?.ToString();

            var parts = new List<string>();

            // İsim + Soyisim (personel, kullanıcı)
            var name = GetStr("Name");
            var surname = GetStr("Surname");
            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(surname))
                parts.Add($"{name} {surname}".Trim());

            // E-posta
            var email = GetStr("Email");
            if (!string.IsNullOrEmpty(email) && !parts.Any())
                parts.Add(email);

            // Kaza tarihi
            var dateVal = props.FirstOrDefault(p => p.Name == "AccidentDate")?.GetValue(request);
            if (dateVal is DateTime dt) parts.Add(dt.ToString("dd.MM.yyyy"));
            else if (dateVal != null && DateTime.TryParse(dateVal.ToString(), out var dtParsed)) parts.Add(dtParsed.ToString("dd.MM.yyyy"));

            // Kaza türü
            var accType = GetStr("TypeOfAccident");
            if (!string.IsNullOrEmpty(accType)) parts.Add(accType);

            // Müdürlük
            var dir = GetStr("Directorate");
            if (!string.IsNullOrEmpty(dir)) parts.Add(dir);

            // Yıl / Ay (istatistik)
            var year = GetStr("Year");
            var month = GetStr("Month");
            if (!string.IsNullOrEmpty(year)) parts.Add(year);
            if (!string.IsNullOrEmpty(month)) parts.Add($"Ay:{month}");

            // Açıklama (kısa)
            if (parts.Count == 0)
            {
                var desc = GetStr("Description");
                if (!string.IsNullOrEmpty(desc))
                    parts.Add(desc.Length > 50 ? desc[..50] + "..." : desc);
            }

            // ID (son çare)
            if (parts.Count == 0)
            {
                var id = GetStr("Id") ?? GetStr("UserId") ?? GetStr("PersonnelId");
                if (!string.IsNullOrEmpty(id))
                    parts.Add($"ID:{(id.Length > 8 ? id[..8] : id)}...");
            }

            return string.Join(", ", parts);
        }
    }
}
