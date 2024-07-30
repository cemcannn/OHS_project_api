using MediatR;

namespace OHS_program_api.Application.Features.Commands.Safety.ActualDailyWage.UpdateActualDailyWage
{
    public class UpdateActualDailyWageCommandRequest : IRequest<UpdateActualDailyWageCommandResponse>
    {
        public string Id { get; set; }
        public string? Month { get; set; }
        public string? Year { get; set; }
        public string? Directorate { get; set; }
        public string? ActualWageSurface { get; set; }
        public string? ActualWageUnderground { get; set; }
        public string? EmployeesNumberSurface { get; set; }
        public string? EmployeesNumberUnderground { get; set; }
    }
}
