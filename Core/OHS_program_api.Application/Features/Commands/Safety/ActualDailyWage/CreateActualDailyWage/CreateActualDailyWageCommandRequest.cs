using MediatR;

namespace OHS_program_api.Application.Features.Commands.Safety.ActualDailyWage.CreateActualDailyWage
{
    public class CreateActualDailyWageCommandRequest : IRequest<CreateActualDailyWageCommandResponse>
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
