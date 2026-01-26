using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OHS_program_api.Infrastructure.Services.ExcelImport
{
    /// <summary>
    /// Belirli aralıklarla Excel dosyalarını kontrol edip otomatik import yapan background servis
    /// </summary>
    public class ExcelAutoImportBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ExcelAutoImportBackgroundService> _logger;
        private readonly string _excelFolderPath;
        private readonly int _checkIntervalSeconds;

        public ExcelAutoImportBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<ExcelAutoImportBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            
            // Excel dosyalarının bulunduğu klasör (API projesi içinde)
            _excelFolderPath = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName ?? "",
                "ExcelDataImport",
                "ExcelFiles");
            
            // Her 60 saniyede bir kontrol et (opsiyonel: appsettings.json'dan okunabilir)
            _checkIntervalSeconds = 60;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Excel Auto Import Background Service başlatıldı.");
            _logger.LogInformation($"Excel klasörü: {_excelFolderPath}");

            // Klasör yoksa oluştur
            if (!Directory.Exists(_excelFolderPath))
            {
                Directory.CreateDirectory(_excelFolderPath);
                _logger.LogInformation($"Excel klasörü oluşturuldu: {_excelFolderPath}");
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessExcelFilesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Excel dosyaları işlenirken hata oluştu.");
                }

                // Belirli süre bekle
                await Task.Delay(TimeSpan.FromSeconds(_checkIntervalSeconds), stoppingToken);
            }
        }

        private async Task ProcessExcelFilesAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var excelReaderService = scope.ServiceProvider.GetRequiredService<IExcelReaderService>();
                var excelImportService = scope.ServiceProvider.GetRequiredService<IExcelImportService>();

                // Veri Yevmiye dosyasını işle
                await ProcessAccidentStatisticsFile(excelReaderService, excelImportService);

                // Veri (Kaza) dosyasını işle
                await ProcessAccidentsFile(excelReaderService, excelImportService);
            }
        }

        private async Task ProcessAccidentStatisticsFile(
            IExcelReaderService excelReaderService,
            IExcelImportService excelImportService)
        {
            var fileName = "Veri Yevmiye.xlsx";
            var filePath = Path.Combine(_excelFolderPath, fileName);

            if (File.Exists(filePath))
            {
                try
                {
                    _logger.LogInformation($"'{fileName}' dosyası işleniyor...");
                    
                    var data = await excelReaderService.ReadAccidentStatisticsAsync(filePath);
                    
                    if (data.Any())
                    {
                        await excelImportService.ImportAccidentStatisticsAsync(data);
                        _logger.LogInformation($"'{fileName}' başarıyla işlendi. {data.Count} satır okundu.");
                        
                        // İşlenen dosyayı arşivle
                        ArchiveProcessedFile(filePath, fileName);
                    }
                    else
                    {
                        _logger.LogWarning($"'{fileName}' dosyasında veri bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"'{fileName}' dosyası işlenirken hata oluştu.");
                }
            }
        }

        private async Task ProcessAccidentsFile(
            IExcelReaderService excelReaderService,
            IExcelImportService excelImportService)
        {
            var fileName = "Veri.xlsx";
            var filePath = Path.Combine(_excelFolderPath, fileName);

            if (File.Exists(filePath))
            {
                try
                {
                    _logger.LogInformation($"'{fileName}' dosyası işleniyor...");
                    
                    var data = await excelReaderService.ReadAccidentsAsync(filePath);
                    
                    if (data.Any())
                    {
                        await excelImportService.ImportAccidentsAsync(data);
                        _logger.LogInformation($"'{fileName}' başarıyla işlendi. {data.Count} satır okundu.");
                        
                        // İşlenen dosyayı arşivle
                        ArchiveProcessedFile(filePath, fileName);
                    }
                    else
                    {
                        _logger.LogWarning($"'{fileName}' dosyasında veri bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"'{fileName}' dosyası işlenirken hata oluştu.");
                }
            }
        }

        private void ArchiveProcessedFile(string filePath, string fileName)
        {
            try
            {
                // Arşiv klasörü oluştur
                var archiveFolder = Path.Combine(_excelFolderPath, "Processed");
                if (!Directory.Exists(archiveFolder))
                {
                    Directory.CreateDirectory(archiveFolder);
                }

                // Dosya ismini tarih-saat ile arşivle
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var archiveFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{timestamp}{Path.GetExtension(fileName)}";
                var archivePath = Path.Combine(archiveFolder, archiveFileName);

                // Dosyayı arşive taşı
                File.Move(filePath, archivePath, overwrite: true);
                _logger.LogInformation($"Dosya arşivlendi: {archiveFileName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Dosya arşivlenirken hata oluştu: {fileName}");
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Excel Auto Import Background Service durduruluyor...");
            return base.StopAsync(cancellationToken);
        }
    }
}
