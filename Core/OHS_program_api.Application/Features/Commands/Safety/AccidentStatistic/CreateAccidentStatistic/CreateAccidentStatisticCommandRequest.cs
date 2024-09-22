using MediatR;

namespace OHS_program_api.Application.Features.Commands.Safety.AccidentStatistic.CreateAccidentStatistic
{
    public class CreateAccidentStatisticCommandRequest : IRequest<CreateAccidentStatisticCommandResponse>
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
