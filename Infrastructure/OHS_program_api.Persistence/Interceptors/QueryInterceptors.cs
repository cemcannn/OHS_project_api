using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Data.Common;

namespace OHS_program_api.Persistence.Interceptors
{
    /// <summary>
    /// Yavaş çalışan sorguları loglayan interceptor
    /// </summary>
    public class SlowQueryInterceptor : DbCommandInterceptor
    {
        private readonly ILogger<SlowQueryInterceptor> _logger;
        private readonly int _slowQueryThresholdMs;

        public SlowQueryInterceptor(ILogger<SlowQueryInterceptor> logger, int slowQueryThresholdMs = 1000)
        {
            _logger = logger;
            _slowQueryThresholdMs = slowQueryThresholdMs;
        }

        public override DbDataReader ReaderExecuted(
            DbCommand command,
            CommandExecutedEventData eventData,
            DbDataReader result)
        {
            if (eventData.Duration.TotalMilliseconds > _slowQueryThresholdMs)
            {
                _logger.LogWarning(
                    "Slow Query Detected! Duration: {Duration}ms\nQuery: {Query}\nParameters: {Parameters}",
                    eventData.Duration.TotalMilliseconds,
                    command.CommandText,
                    GetParameterValues(command));
            }

            return base.ReaderExecuted(command, eventData, result);
        }

        public override async ValueTask<DbDataReader> ReaderExecutedAsync(
            DbCommand command,
            CommandExecutedEventData eventData,
            DbDataReader result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Duration.TotalMilliseconds > _slowQueryThresholdMs)
            {
                _logger.LogWarning(
                    "Slow Query Detected! Duration: {Duration}ms\nQuery: {Query}\nParameters: {Parameters}",
                    eventData.Duration.TotalMilliseconds,
                    command.CommandText,
                    GetParameterValues(command));
            }

            return await base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
        }

        private string GetParameterValues(DbCommand command)
        {
            var parameters = command.Parameters
                .Cast<DbParameter>()
                .Select(p => $"{p.ParameterName}={p.Value}")
                .ToList();

            return string.Join(", ", parameters);
        }
    }

    /// <summary>
    /// Query statistics toplayan interceptor
    /// </summary>
    public class QueryStatisticsInterceptor : DbCommandInterceptor
    {
        private static readonly ConcurrentDictionary<string, QueryStats> _statistics = new();
        private readonly ILogger<QueryStatisticsInterceptor> _logger;

        public QueryStatisticsInterceptor(ILogger<QueryStatisticsInterceptor> logger)
        {
            _logger = logger;
        }

        public override DbDataReader ReaderExecuted(
            DbCommand command,
            CommandExecutedEventData eventData,
            DbDataReader result)
        {
            RecordStatistics(command.CommandText, eventData.Duration.TotalMilliseconds);
            return base.ReaderExecuted(command, eventData, result);
        }

        public override async ValueTask<DbDataReader> ReaderExecutedAsync(
            DbCommand command,
            CommandExecutedEventData eventData,
            DbDataReader result,
            CancellationToken cancellationToken = default)
        {
            RecordStatistics(command.CommandText, eventData.Duration.TotalMilliseconds);
            return await base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
        }

        private void RecordStatistics(string query, double durationMs)
        {
            var key = GetQueryKey(query);
            
            _statistics.AddOrUpdate(
                key,
                new QueryStats { Count = 1, TotalDuration = durationMs, MaxDuration = durationMs, MinDuration = durationMs },
                (_, stats) =>
                {
                    stats.Count++;
                    stats.TotalDuration += durationMs;
                    stats.MaxDuration = Math.Max(stats.MaxDuration, durationMs);
                    stats.MinDuration = Math.Min(stats.MinDuration, durationMs);
                    return stats;
                });
        }

        private string GetQueryKey(string query)
        {
            // Sorgu metnini normalize et (parametre değerlerini kaldır)
            return query.Length > 100 ? query.Substring(0, 100) : query;
        }

        public static Dictionary<string, QueryStats> GetStatistics()
        {
            return _statistics.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static void ResetStatistics()
        {
            _statistics.Clear();
        }
    }

    public class QueryStats
    {
        public int Count { get; set; }
        public double TotalDuration { get; set; }
        public double MaxDuration { get; set; }
        public double MinDuration { get; set; }
        public double AverageDuration => Count > 0 ? TotalDuration / Count : 0;
    }
}
