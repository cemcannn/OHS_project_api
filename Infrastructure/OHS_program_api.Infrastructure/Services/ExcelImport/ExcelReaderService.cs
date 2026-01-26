using OfficeOpenXml;
using System.Globalization;

namespace OHS_program_api.Infrastructure.Services.ExcelImport
{
    /// <summary>
    /// Excel dosyalarından veri okuma ve işleme servisi
    /// </summary>
    public interface IExcelReaderService
    {
        Task<List<AccidentStatisticExcelData>> ReadAccidentStatisticsAsync(string filePath);
        Task<List<AccidentExcelData>> ReadAccidentsAsync(string filePath);
    }

    public class ExcelReaderService : IExcelReaderService
    {
        public ExcelReaderService()
        {
            // EPPlus lisans ayarı
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        /// <summary>
        /// Veri Yevmiye Excel dosyasını okur
        /// </summary>
        public async Task<List<AccidentStatisticExcelData>> ReadAccidentStatisticsAsync(string filePath)
        {
            var result = new List<AccidentStatisticExcelData>();

            if (!File.Exists(filePath))
                return result;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // İlk sekme
                var rowCount = worksheet.Dimension?.Rows ?? 0;

                // Başlık satırını atla, 2'den başla
                for (int row = 2; row <= rowCount; row++)
                {
                    try
                    {
                        var year = GetCellValue(worksheet, row, 1);
                        var month = GetCellValue(worksheet, row, 2);
                        var directorate = GetCellValue(worksheet, row, 3);
                        var employeesUnderground = GetCellValue(worksheet, row, 4);
                        var employeesSurface = GetCellValue(worksheet, row, 5);
                        var dailyWageUnderground = GetCellValue(worksheet, row, 6);
                        var dailyWageSurface = GetCellValue(worksheet, row, 7);

                        // Boş satırı atla
                        if (string.IsNullOrWhiteSpace(year) && string.IsNullOrWhiteSpace(month))
                            continue;

                        var data = new AccidentStatisticExcelData
                        {
                            Year = year,
                            Month = TKICodeMappings.GetMonthNumber(month),
                            Directorate = directorate,
                            EmployeesNumberUnderground = CleanNumber(employeesUnderground),
                            EmployeesNumberSurface = CleanNumber(employeesSurface),
                            ActualDailyWageUnderground = CleanNumber(dailyWageUnderground),
                            ActualDailyWageSurface = CleanNumber(dailyWageSurface)
                        };

                        result.Add(data);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Satır {row} işlenirken hata: {ex.Message}");
                        continue;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Veri (Kaza Verileri) Excel dosyasını okur
        /// </summary>
        public async Task<List<AccidentExcelData>> ReadAccidentsAsync(string filePath)
        {
            var result = new List<AccidentExcelData>();

            if (!File.Exists(filePath))
                return result;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // İlk sekme
                var rowCount = worksheet.Dimension?.Rows ?? 0;

                // Başlık satırını atla, 2'den başla
                for (int row = 2; row <= rowCount; row++)
                {
                    try
                    {
                        var registrationNo = GetCellValue(worksheet, row, 1); // Sicil No
                        var name = GetCellValue(worksheet, row, 2); // Adı
                        var surname = GetCellValue(worksheet, row, 3); // Soyadı
                        var directorate = GetCellValue(worksheet, row, 4); // İşletme
                        var birthDate = GetCellValue(worksheet, row, 5); // Doğum Tarihi
                        var professionCode = GetCellValue(worksheet, row, 6); // Sanatı (kod)
                        var accidentDate = GetCellValue(worksheet, row, 7); // Kaza Tarihi
                        var accidentHour = GetCellValue(worksheet, row, 8); // Saat
                        var accidentAreaCode = GetCellValue(worksheet, row, 9); // Yer (kod)
                        var typeOfAccidentCode = GetCellValue(worksheet, row, 10); // Neden (kod)
                        var limbCode = GetCellValue(worksheet, row, 11); // Uzuv (kod)
                        var lostDays = GetCellValue(worksheet, row, 12); // Gün Kaybı
                        var description = GetCellValue(worksheet, row, 13); // Kazanın Kısa Açıklaması

                        // Boş satırı atla
                        if (string.IsNullOrWhiteSpace(registrationNo))
                            continue;

                        var data = new AccidentExcelData
                        {
                            TKIId = CleanSicilNo(registrationNo),
                            Name = FormatName(name),
                            Surname = FormatName(surname),
                            Directorate = directorate,
                            BornDate = ParseDate(birthDate),
                            Profession = TKICodeMappings.GetProfessionName(ParseInt(professionCode)),
                            AccidentDate = ParseDate(accidentDate),
                            AccidentHour = accidentHour,
                            AccidentArea = TKICodeMappings.GetAccidentAreaName(ParseInt(accidentAreaCode)),
                            TypeOfAccident = TKICodeMappings.GetTypeOfAccidentName(ParseInt(typeOfAccidentCode)),
                            Limb = TKICodeMappings.GetLimbName(ParseInt(limbCode)),
                            LostDayOfWork = ParseInt(lostDays),
                            Description = description
                        };

                        result.Add(data);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Satır {row} işlenirken hata: {ex.Message}");
                        continue;
                    }
                }
            }

            return result;
        }

        private string GetCellValue(ExcelWorksheet worksheet, int row, int col)
        {
            var cellValue = worksheet.Cells[row, col].Value;
            return cellValue?.ToString()?.Trim() ?? string.Empty;
        }

        private int CleanNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return 0;

            // Virgül ve boşlukları temizle
            value = value.Replace(",", "").Replace(" ", "").Trim();

            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
            {
                return (int)Math.Round(number);
            }

            return 0;
        }

        private int ParseInt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return 0;

            if (int.TryParse(value, out int result))
                return result;

            return 0;
        }

        private DateTime? ParseDate(string dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr))
                return null;

            // DD.MM.YYYY formatını dene
            if (DateTime.TryParseExact(dateStr, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date1))
                return date1;

            // Diğer formatları dene
            if (DateTime.TryParse(dateStr, out DateTime date2))
                return date2;

            return null;
        }

        private string FormatName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            // Tüm harfleri küçült
            name = name.ToLower(new CultureInfo("tr-TR"));
            
            // İlk harfi büyüt
            if (name.Length > 0)
            {
                name = char.ToUpper(name[0], new CultureInfo("tr-TR")) + name.Substring(1);
            }

            return name;
        }

        private string CleanSicilNo(string sicilNo)
        {
            if (string.IsNullOrWhiteSpace(sicilNo))
                return string.Empty;

            return sicilNo.Replace(",", "").Trim();
        }
    }

    // Excel'den okunan veri modelleri
    public class AccidentStatisticExcelData
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public string Directorate { get; set; }
        public int EmployeesNumberUnderground { get; set; }
        public int EmployeesNumberSurface { get; set; }
        public int ActualDailyWageUnderground { get; set; }
        public int ActualDailyWageSurface { get; set; }
    }

    public class AccidentExcelData
    {
        public string TKIId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Directorate { get; set; }
        public DateTime? BornDate { get; set; }
        public string Profession { get; set; }
        public DateTime? AccidentDate { get; set; }
        public string AccidentHour { get; set; }
        public string AccidentArea { get; set; }
        public string TypeOfAccident { get; set; }
        public string Limb { get; set; }
        public int LostDayOfWork { get; set; }
        public string Description { get; set; }
    }
}
