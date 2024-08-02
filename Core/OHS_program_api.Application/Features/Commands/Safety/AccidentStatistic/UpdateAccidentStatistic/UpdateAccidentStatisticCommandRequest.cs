using MediatR;

namespace OHS_program_api.Application.Features.Commands.Safety.AccidentStatistic.UpdateAccidentStatistic
{
    public class UpdateAccidentStatisticCommandRequest : IRequest<UpdateAccidentStatisticCommandResponse>
    {
        public string Id { get; set; }
        public string? Month { get; set; }
        public string? Year { get; set; }
        public string? Directorate { get; set; }
        public string? ActualDailyWageSurface { get; set; }
        public string? ActualDailyWageUnderground { get; set; }
        public string? EmployeesNumberSurface { get; set; }
        public string? EmployeesNumberUnderground { get; set; }
    }
}
