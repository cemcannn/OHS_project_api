using MediatR;

namespace OHS_program_api.Application.Features.Commands.Safety.AccidentStatistic.UpdateAccidentStatistic
{
    public class UpdateAccidentStatisticCommandRequest : IRequest<UpdateAccidentStatisticCommandResponse>
    {
        public string Id { get; set; }
        public string? Month { get; set; }
        public string? Year { get; set; }
        public string? Directorate { get; set; }
        public int? ActualDailyWageSurface { get; set; }
        public int? ActualDailyWageUnderground { get; set; }
        public int? EmployeesNumberSurface { get; set; }
        public int? EmployeesNumberUnderground { get; set; }
    }
}
