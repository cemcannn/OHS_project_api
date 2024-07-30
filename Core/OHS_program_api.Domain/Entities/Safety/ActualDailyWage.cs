using OHS_program_api.Domain.Entities.Common;

namespace OHS_program_api.Domain.Entities.Safety
{
    public class ActualDailyWage : BaseEntity
    {
        public string? Month { get; set; }
        public string? Year { get; set; }
        public string? Directorate { get; set; }
        public string? ActualWageSurface { get; set; }
        public string? ActualWageUnderground { get; set; }
        public string? EmployeesNumberSurface { get; set; }
        public string? EmployeesNumberUnderground { get; set; }
    }
}
