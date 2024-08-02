using OHS_program_api.Domain.Entities.Common;

namespace OHS_program_api.Domain.Entities.Safety
{
    public class AccidentStatistic : BaseEntity
    {
        public string? Month { get; set; }
        public string? Year { get; set; }
        public string? Directorate { get; set; }
        public string? ActualDailyWageSurface { get; set; }
        public string? ActualDailyWageUnderground { get; set; }
        public string? EmployeesNumberSurface { get; set; }
        public string? EmployeesNumberUnderground { get; set; }
    }
}
