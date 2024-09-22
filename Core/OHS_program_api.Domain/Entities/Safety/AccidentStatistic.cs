using OHS_program_api.Domain.Entities.Common;

namespace OHS_program_api.Domain.Entities.Safety
{
    public class AccidentStatistic : BaseEntity
    {
        public string? Month { get; set; }
        public string? Year { get; set; }
        public string? Directorate { get; set; }
        public int? ActualDailyWageSurface { get; set; }
        public int? ActualDailyWageUnderground { get; set; }
        public int? EmployeesNumberSurface { get; set; }
        public int? EmployeesNumberUnderground { get; set; }
    }
}
