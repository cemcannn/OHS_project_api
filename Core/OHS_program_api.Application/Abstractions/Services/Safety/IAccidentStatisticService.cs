using OHS_program_api.Application.ViewModels.Safety.Accidents;
using OHS_program_api.Application.ViewModels.Safety.AccidentStatistic;

namespace OHS_program_api.Application.Abstractions.Services.Safety
{
    public interface IAccidentStatisticService
    {
        Task<(List<VM_List_AccidentStatistic> accidentStatistics, int totalCount)> GetAllAccidentStatisticsAsync();
    }
}
