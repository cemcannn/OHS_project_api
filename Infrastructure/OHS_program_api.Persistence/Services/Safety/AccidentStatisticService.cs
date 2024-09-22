using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Abstractions.Services.Safety;
using OHS_program_api.Application.Repositories.Safety.AccidentRepository;
using OHS_program_api.Application.Repositories.Safety.AccidentStatisticRepository;
using OHS_program_api.Application.ViewModels.Safety.AccidentStatistic;
using OHS_program_api.Domain.Entities.OccupationalSafety;

public class AccidentStatisticService : IAccidentStatisticService
{
    private readonly IAccidentStatisticReadRepository _accidentStatisticReadRepository;
    private readonly IAccidentReadRepository _accidentReadRepository;

    public AccidentStatisticService(IAccidentStatisticReadRepository accidentStatisticReadRepository, IAccidentReadRepository accidentReadRepository)
    {
        _accidentStatisticReadRepository = accidentStatisticReadRepository;
        _accidentReadRepository = accidentReadRepository;
    }

    public async Task<(List<VM_List_AccidentStatistic> accidentStatistics, int totalCount)> GetAllAccidentStatisticsAsync()
    {
        // Veriyi çekiyoruz, ancak LostDayOfWork hesaplamasını burada yapmıyoruz
        var accidentStatistics = _accidentStatisticReadRepository.GetAll(false)
            .Select(p => new
            {
                p.Id,
                p.Month,
                p.Year,
                p.Directorate,
                p.ActualDailyWageSurface,
                p.ActualDailyWageUnderground,
                p.EmployeesNumberSurface,
                p.EmployeesNumberUnderground
            }).ToList();

        var totalCount = accidentStatistics.Count;

        // İş günü kayıplarını veriyi çektikten sonra hesaplıyoruz
    var result = accidentStatistics.Select(p => new VM_List_AccidentStatistic
    {
        Id = p.Id.ToString(),
        Month = p.Month,
        Year = p.Year,
        Directorate = p.Directorate,
        ActualDailyWageSurface = p.ActualDailyWageSurface,
        ActualDailyWageUnderground = p.ActualDailyWageUnderground,
        
        // ActualDailyWage toplamı
        ActualDailyWageSummary = (p.ActualDailyWageSurface + p.ActualDailyWageUnderground),

        // EmployeesNumber toplamı
        EmployeesNumberSummary = (p.EmployeesNumberSurface+ p.EmployeesNumberUnderground),

        // Çalışma saatleri hesaplamaları
        WorkingHoursSurface = (p.ActualDailyWageSurface * 8),
        WorkingHoursUnderground = (p.ActualDailyWageUnderground * 8),
        WorkingHoursSummary = ((p.ActualDailyWageSurface * 8) + (p.ActualDailyWageUnderground * 8)),

        // İş günü kayıplarını hesaplıyoruz
        LostDayOfWorkSummary = CalculateLostDayOfWorkSummary(p.Month, p.Year, p.Directorate)
    }).ToList();

    return (result, totalCount);
}

    private int CalculateLostDayOfWorkSummary(string month, string year, string directorate)
    {
        // Ayı ve yılı sayısal olarak dönüştür
        int monthNumber = int.Parse(month);
        int yearNumber = int.Parse(year);

        // Bu ay, yıl ve işletmeye karşılık gelen kazaları alıyoruz
        var accidentsInMonth = _accidentReadRepository.GetAll(false)
            .Include(a => a.Personnel) // Personnel'ı dahil ediyoruz
            .Where(a => a.AccidentDate.HasValue
                        && a.AccidentDate.Value.Month == monthNumber // Sayısal karşılaştırma
                        && a.AccidentDate.Value.Year == yearNumber // Sayısal karşılaştırma
                        && a.Personnel != null
                        && a.Personnel.Directorate == directorate) // İşletme filtrelemesi
            .ToList();

        // İş günü kayıplarını topluyoruz
        var totalLostDays = accidentsInMonth.Sum(a => a.LostDayOfWork ?? 0);

        return totalLostDays;
    }


}
