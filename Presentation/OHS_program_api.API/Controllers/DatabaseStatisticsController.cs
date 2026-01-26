using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OHS_program_api.Persistence.Interceptors;
using OHS_program_api.Persistence.Services;

namespace OHS_program_api.API.Controllers
{
    /// <summary>
    /// Veritabanı performans ve istatistik API'si
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class DatabaseStatisticsController : ControllerBase
    {
        private readonly IDataArchivingService _archivingService;
        private readonly ILogger<DatabaseStatisticsController> _logger;

        public DatabaseStatisticsController(
            IDataArchivingService archivingService,
            ILogger<DatabaseStatisticsController> logger)
        {
            _archivingService = archivingService;
            _logger = logger;
        }

        /// <summary>
        /// Sorgu performans istatistiklerini getirir
        /// </summary>
        [HttpGet("query-statistics")]
        public IActionResult GetQueryStatistics()
        {
            var stats = QueryStatisticsInterceptor.GetStatistics();
            
            var topSlowQueries = stats
                .OrderByDescending(s => s.Value.AverageDuration)
                .Take(10)
                .Select(s => new
                {
                    Query = s.Key,
                    Count = s.Value.Count,
                    AvgDuration = Math.Round(s.Value.AverageDuration, 2),
                    MaxDuration = Math.Round(s.Value.MaxDuration, 2),
                    MinDuration = Math.Round(s.Value.MinDuration, 2),
                    TotalDuration = Math.Round(s.Value.TotalDuration, 2)
                })
                .ToList();

            return Ok(new
            {
                TotalQueries = stats.Sum(s => s.Value.Count),
                UniqueQueries = stats.Count,
                TopSlowQueries = topSlowQueries
            });
        }

        /// <summary>
        /// Sorgu istatistiklerini sıfırlar
        /// </summary>
        [HttpPost("query-statistics/reset")]
        public IActionResult ResetQueryStatistics()
        {
            QueryStatisticsInterceptor.ResetStatistics();
            return Ok(new { Message = "Query statistics reset successfully" });
        }

        /// <summary>
        /// Eski kazaları arşivler
        /// </summary>
        [HttpPost("archive/accidents")]
        public async Task<IActionResult> ArchiveOldAccidents([FromQuery] int olderThanYears = 5)
        {
            var count = await _archivingService.ArchiveOldAccidentsAsync(olderThanYears);
            return Ok(new { ArchivedCount = count, OlderThanYears = olderThanYears });
        }

        /// <summary>
        /// Soft delete edilmiş kayıtları kalıcı olarak siler
        /// </summary>
        [HttpPost("archive/deleted-records")]
        public async Task<IActionResult> ArchiveDeletedRecords([FromQuery] int olderThanDays = 90)
        {
            var count = await _archivingService.ArchiveDeletedRecordsAsync(olderThanDays);
            return Ok(new { DeletedCount = count, OlderThanDays = olderThanDays });
        }

        /// <summary>
        /// Arşiv istatistiklerini getirir
        /// </summary>
        [HttpGet("archive/statistics")]
        public async Task<IActionResult> GetArchiveStatistics()
        {
            var stats = await _archivingService.GetArchiveStatisticsAsync();
            return Ok(stats);
        }
    }
}
