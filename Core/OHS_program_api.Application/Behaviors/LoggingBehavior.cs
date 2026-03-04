using MediatR;
using Microsoft.AspNetCore.Http;
using OHS_program_api.Application.Abstractions.Hubs;
using OHS_program_api.Application.Abstractions.Services;
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
            var userId = claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            var userName = claims?.FindFirst(ClaimTypes.Name)?.Value ?? "Bilinmeyen";

            var action = BuildMessage(userName, requestName);

            try
            {
                await _auditLogService.LogAsync(userId, userName, action, requestName);
                await _hubService.SendActivityAsync(action);
            }
            catch { /* Loglama hatası ana işlemi engellemez */ }

            return response;
        }

        private static string BuildMessage(string userName, string requestTypeName)
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
                var s when s.Contains("Role") => "rol",
                var s when s.Contains("User") => "kullanıcı",
                var s when s.Contains("ProfilePhoto") => "profil fotoğrafı",
                var s when s.Contains("Password") => "şifre",
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

            return $"{userName} {entity} {verb}";
        }
    }
}
