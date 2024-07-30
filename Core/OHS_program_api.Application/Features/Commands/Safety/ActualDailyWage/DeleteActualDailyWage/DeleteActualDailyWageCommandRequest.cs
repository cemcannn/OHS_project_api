using MediatR;

namespace OHS_program_api.Application.Features.Commands.Safety.ActualDailyWage.DeleteActualDailyWage
{
    public class DeleteActualDailyWageCommandRequest : IRequest<DeleteActualDailyWageCommandResponse>
    {
        public string Id { get; set; }
    }
}
