using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Abstractions.Services.Safety;
using OHS_program_api.Application.Repositories.Safety.AccidentRepository;
using OHS_program_api.Application.Repositories.Safety.AccidentStatisticRepository;
using OHS_program_api.Application.ViewModels.Safety.AccidentStatistic;

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
        var accidentStatistics = await _accidentStatisticReadRepository.GetAll(false)
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
            })
            .ToListAsync();

        var totalCount = accidentStatistics.Count;

        var result = accidentStatistics.Select(p => new VM_List_AccidentStatistic
        {
            Id = p.Id.ToString(),
            Month = p.Month,
            Year = p.Year,
            Directorate = p.Directorate,
            ActualDailyWageSurface = p.ActualDailyWageSurface,
            ActualDailyWageUnderground = p.ActualDailyWageUnderground,
            EmployeesNumberSurface = p.EmployeesNumberSurface,
            EmployeesNumberUnderground = p.EmployeesNumberUnderground,
            LostDayOfWorkSummary = CalculateLostDayOfWorkSummary(p.Month, p.Year, p.Directorate),

        }).ToList();

        return (result, totalCount);
    }

    private int CalculateLostDayOfWorkSummary(string? month, string? year, string? directorate)
    {
        if (string.IsNullOrWhiteSpace(month) || string.IsNullOrWhiteSpace(year) || string.IsNullOrWhiteSpace(directorate))
            return 0;

        if (!int.TryParse(month, out int monthNumber) || !int.TryParse(year, out int yearNumber))
            return 0;

        var accidentsInMonth = _accidentReadRepository.GetAll(false)
            .Include(a => a.Personnel) 
            .Where(a => a.AccidentDate.HasValue
                        && a.AccidentDate.Value.Month == monthNumber 
                        && a.AccidentDate.Value.Year == yearNumber 
                        && a.Personnel != null
                        && a.Personnel.Directorate == directorate) 
            .ToList();

        var totalLostDays = accidentsInMonth.Sum(a => a.LostDayOfWork ?? 0);

        return totalLostDays;
    }


}
