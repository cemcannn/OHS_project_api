namespace OHS_program_api.Application.ViewModels.Safety.AccidentStatistic
{
    public class VM_List_AccidentStatistic
    {
        public string? Id { get; set; }
        public string? Month { get; set; }
        public string? Year { get; set; }
        public string? Directorate { get; set; }
        public int? ActualDailyWageSurface { get; set; }
        public int? ActualDailyWageUnderground { get; set; }
        public int? ActualDailyWageSummary { get; set; }
        public int? EmployeesNumberSurface { get; set; }
        public int? EmployeesNumberUnderground { get; set; }
        public int? EmployeesNumberSummary { get; set; }
        public int? WorkingHoursSurface { get; set; }
        public int? WorkingHoursUnderground { get; set; }
        public int? WorkingHoursSummary { get; set; }
        public int? LostDayOfWorkSummary { get; set; }
        public double? AccidentSeverityRate { get; set; }
    }
}
