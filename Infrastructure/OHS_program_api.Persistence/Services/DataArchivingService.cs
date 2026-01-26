using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OHS_program_api.Persistence.Contexts;

namespace OHS_program_api.Persistence.Services
{
    /// <summary>
    /// Eski verileri arşivleme servisi
    /// </summary>
    public interface IDataArchivingService
    {
        Task<int> ArchiveOldAccidentsAsync(int olderThanYears = 5);
        Task<int> ArchiveDeletedRecordsAsync(int olderThanDays = 90);
        Task<Dictionary<string, int>> GetArchiveStatisticsAsync();
    }

    public class DataArchivingService : IDataArchivingService
    {
        private readonly OHSProgramAPIDbContext _context;
        private readonly ILogger<DataArchivingService> _logger;

        public DataArchivingService(OHSProgramAPIDbContext context, ILogger<DataArchivingService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Belirli yıldan eski kazaları arşivler
        /// </summary>
        public async Task<int> ArchiveOldAccidentsAsync(int olderThanYears = 5)
        {
            try
            {
                var cutoffDate = DateTime.UtcNow.AddYears(-olderThanYears);
                
                // SQL komutu ile toplu arşivleme (performanslı)
                var sql = @"
                    INSERT INTO AccidentArchive 
                    SELECT * FROM Accidents 
                    WHERE AccidentDate < @p0 AND IsDeleted = false";

                var archivedCount = await _context.Database.ExecuteSqlRawAsync(sql, cutoffDate);

                // Arşivlenen kayıtları sil
                var deleteSql = @"
                    DELETE FROM Accidents 
                    WHERE AccidentDate < @p0 AND IsDeleted = false";
                
                await _context.Database.ExecuteSqlRawAsync(deleteSql, cutoffDate);

                _logger.LogInformation(
                    "Archived {Count} accidents older than {Years} years",
                    archivedCount,
                    olderThanYears);

                return archivedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error archiving old accidents");
                throw;
            }
        }

        /// <summary>
        /// Soft delete edilmiş kayıtları kalıcı olarak siler
        /// </summary>
        public async Task<int> ArchiveDeletedRecordsAsync(int olderThanDays = 90)
        {
            try
            {
                var cutoffDate = DateTime.UtcNow.AddDays(-olderThanDays);
                
                var sql = @"
                    DELETE FROM Accidents 
                    WHERE IsDeleted = true AND DeletedDate < @p0";

                var deletedCount = await _context.Database.ExecuteSqlRawAsync(sql, cutoffDate);

                _logger.LogInformation(
                    "Permanently deleted {Count} soft-deleted records older than {Days} days",
                    deletedCount,
                    olderThanDays);

                return deletedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting soft-deleted records");
                throw;
            }
        }

        /// <summary>
        /// Arşiv istatistiklerini getirir
        /// </summary>
        public async Task<Dictionary<string, int>> GetArchiveStatisticsAsync()
        {
            var stats = new Dictionary<string, int>();

            try
            {
                var activeAccidents = await _context.Database
                    .ExecuteSqlRawAsync("SELECT COUNT(*) FROM Accidents WHERE IsDeleted = false");
                
                var deletedAccidents = await _context.Database
                    .ExecuteSqlRawAsync("SELECT COUNT(*) FROM Accidents WHERE IsDeleted = true");

                stats["ActiveAccidents"] = activeAccidents;
                stats["DeletedAccidents"] = deletedAccidents;

                return stats;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting archive statistics");
                return stats;
            }
        }
    }
}
